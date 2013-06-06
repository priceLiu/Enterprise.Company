﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class SqlTable : ITable
	{
		protected Type type;
		protected string name;
		protected string schema;
		protected List<SqlColumn> columns;
		protected SqlColumn identity;
		protected List<SqlTrigger> triggers;
		protected bool isSPResult;
		
		private string insertSQL;
		private string updateSQL;
		private string deleteSQL;
		private string selectSQL;
		
		public SqlTable(Type type, string name, string schema)
		{
			this.type = type;
			if (name != null)
				this.name = name.ToLower();
			if (schema != null)
				this.schema = schema.ToLower();
			columns = new List<SqlColumn>();
			triggers = new List<SqlTrigger>();
			isSPResult = (name == null || name.Length == 0);
		}
		
		public Type Class
		{
			get { return type; }
		}
		
		public string Name
		{
			get { return name; }
		}
		
		public string Schema
		{
			get { return schema; }
		}
		
		public IColumn[] Columns
		{
			get { return columns.ToArray(); }
		}
		
		public virtual void Add(SqlColumn column)
		{
			if (column.IsID)
			{
				if (identity != null)
				{
					throw new LightException(string.Format(
						"cannot add idenity column {0} to table {1}, it already has an identity column {2}",
						column.Name, this.Name, identity.Name));
				}
				identity = column;
			}
			columns.Add(column);
			column.Ordinal = columns.Count;
		}
		
		public virtual void AddTrigger(SqlTrigger trigger)
		{
			triggers.Add(trigger);
		}
		
		public SqlColumn ByName(string name)
		{
			if (name == null || name.Length == 0)
				return null;
			string target = name.ToLower();
			foreach (SqlColumn column in columns)
			{
				if (column.Name.Equals(target))
					return column;
			}
			return null;
		}
		
		public SqlColumn ByAlias(string alias)
		{
			if (alias == null || alias.Length == 0)
				return null;
			string target = alias.ToLower();
			foreach (SqlColumn column in columns)
			{
				if (column.Alias.Equals(target))
					return column;
			}
			return null;
		}
		
		public string Translate(string alias)
		{
			SqlColumn column = ByAlias(alias);
			if (column == null)
				return null;
			return column.Name;
		}
		
		private void FireTriggers(IEnumerable targets, TriggerEventArgs e)
		{
			object[] args = new object[2];
			args[1] = e;
			Timing time = e.Timing;
			foreach (object target in targets)
			{
				args[0] = target;
				foreach (SqlTrigger trigger in triggers)
				{
					if ((trigger.Timing & time) > 0)
						trigger.Fire(target, args);
				}
			}
		}
		
		private void ErrorIfSPResult()
		{
			if (isSPResult)
			{
				string format = "type {0} is marked with SPResultAttribute, not TableAttribute";
				throw new LightException(string.Format(format, type.FullName));
			}
		}
		
		public virtual int Insert(IDb db, ICollection items)
		{
			ErrorIfSPResult();
			int result = 0;
			string sql = GetInsertSql();
			IDbCommand cmd = null;
			IDbDataParameter idP = null;
			FireTriggers(items, new TriggerEventArgs(db, Timing.BeforeInsert));
			try
			{
				cmd = db.Connection.CreateCommand();
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = db.Transaction;
				foreach (SqlColumn column in columns)
				{	// add parameters
					if (column.IsReadable && !column.IsID)
					{
						IDbDataParameter p = cmd.CreateParameter();
						p.ParameterName = string.Format("@{0}", column.Ordinal);
						SqlUtils.PrepParam(p, column.DataType);
						cmd.Parameters.Add(p);
					}
				}
				if (identity != null)
				{	// add output parameter for identity column
					idP = cmd.CreateParameter();
					idP.ParameterName = "@id";
					SqlUtils.PrepParam(idP, identity.DataType);
					idP.Direction = ParameterDirection.Output;
					cmd.Parameters.Add(idP);
				}
				cmd.Prepare();
				foreach (object item in items)
				{
					if (item == null)
						continue;
					foreach (SqlColumn column in columns)
					{	// assign values to all parameters
						if (column.IsReadable && !column.IsID)
						{
							string pname = string.Format("@{0}", column.Ordinal);
							IDbDataParameter p = (IDbDataParameter) cmd.Parameters[pname];
							column.SetParameterValue(p, item);
						}
					}
					
					TraceObject.Instance.TraceCommand(cmd);
					
					result += cmd.ExecuteNonQuery();
					if (identity != null)
					{	// write generated identity value back into the object
						identity.SetValue(item, idP.Value);
					}
				}
			}
			finally
			{
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}
			}
			FireTriggers(items, new TriggerEventArgs(db, Timing.AfterInsert));
			return result;
		}
		
		public virtual int Update(IDb db, ICollection items)
		{
			ErrorIfSPResult();
			int result = 0;
			string sql = GetUpdateSql();
			IDbCommand cmd = null;
			FireTriggers(items, new TriggerEventArgs(db, Timing.BeforeUpdate));
			try
			{
				cmd = db.Connection.CreateCommand();
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = db.Transaction;
				foreach (SqlColumn column in columns)
				{	// add parameters
					if (column.IsReadable && !(column.IsID && !column.IsPK) )
					{
						string prefix = column.IsPK ? "@pk" : "@";
						IDbDataParameter p = cmd.CreateParameter();
						p.ParameterName = string.Format("{0}{1}", prefix, column.Ordinal);
						SqlUtils.PrepParam(p, column.DataType);
						cmd.Parameters.Add(p);
					}
				}
				cmd.Prepare();
				foreach (object item in items)
				{
					if (item != null)
					{
						foreach (SqlColumn column in columns)
						{	// assign values to parameters
							if (column.IsReadable && !(column.IsID && !column.IsPK) )
							{
								string prefix = column.IsPK ? "@pk" : "@";
								string pname = string.Format("{0}{1}", prefix, column.Ordinal);
								IDbDataParameter p = (IDbDataParameter) cmd.Parameters[pname];
								column.SetParameterValue(p, item);
							}
						}
						
						TraceObject.Instance.TraceCommand(cmd);
						
						result += cmd.ExecuteNonQuery();
					}
				}
			}
			finally
			{
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}
			}
			FireTriggers(items, new TriggerEventArgs(db, Timing.AfterUpdate));
			return result;
		}
		
		public virtual int Delete(IDb db, ICollection items)
		{
			ErrorIfSPResult();
			int result = 0;
			string sql = GetDeleteSql();
			IDbCommand cmd = null;
			FireTriggers(items, new TriggerEventArgs(db, Timing.BeforeDelete));
			try
			{
				cmd = db.Connection.CreateCommand();
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = db.Transaction;
				foreach (SqlColumn column in columns)
				{
					if (column.IsPK)
					{
						IDbDataParameter p = cmd.CreateParameter();
						p.ParameterName = string.Format("@pk{0}", column.Ordinal);
						SqlUtils.PrepParam(p, column.DataType);
						cmd.Parameters.Add(p);
					}
				}
				cmd.Prepare();
				foreach (object item in items)
				{
					if (item != null)
					{
						foreach (SqlColumn column in columns)
						{
							if (column.IsPK)
							{
								string pname = string.Format("@pk{0}", column.Ordinal);
								IDbDataParameter p = (IDbDataParameter) cmd.Parameters[pname];
								column.SetParameterValue(p, item);
							}
						}
						
						TraceObject.Instance.TraceCommand(cmd);
						
						result += cmd.ExecuteNonQuery();
					}
				}
			}
			finally
			{
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}
			}
			FireTriggers(items, new TriggerEventArgs(db, Timing.AfterDelete));
			return result;
		}
		
		public virtual int Delete(IDb db, IQuery query)
		{
			ErrorIfSPResult();
			int result = 0;
			SqlQuery sqlquery = (SqlQuery) query; //as SqlQuery;
			string sql = GetDeleteSql(sqlquery);
			IDbCommand cmd = null;
			try
			{
				cmd = db.Connection.CreateCommand();
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = db.Transaction;
				if (sqlquery != null)
					sqlquery.SetParameters(cmd);
				cmd.Prepare();
				
				TraceObject.Instance.TraceCommand(cmd);
				
				result = cmd.ExecuteNonQuery();
			}
			finally
			{
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}
			}
			return result;
		}
		
		public virtual void Select(IDb db, IQuery query, IList list)
		{
			ErrorIfSPResult();
			SqlQuery sqlquery = (SqlQuery) query;
			string sql = GetSelectSql(sqlquery);
			IDbCommand cmd = null;
			IDataReader reader = null;
			try
			{
				cmd = db.Connection.CreateCommand();
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = db.Transaction;
				if (sqlquery != null)
					sqlquery.SetParameters(cmd);
				cmd.Prepare();
				
				TraceObject.Instance.TraceCommand(cmd);

                reader = cmd.ExecuteReader();
                FillByIndex(reader, list);

                #region SqlCacheDependency

                //SqlCacheDependency SqlDep = null;

                //if (string.IsNullOrEmpty(this.schema))
                //{
                //    reader = cmd.ExecuteReader();
                //    FillByIndex(reader, list);
                //}
                //else
                //{
                //    if (ORMCache.Instance.Get(cmd.CommandText.GetHashCode().ToString()) == null)
                //    {
                //        try
                //        {
                //            SqlDep = new SqlCacheDependency((SqlCommand)cmd);
                //        }
                //        catch (DatabaseNotEnabledForNotificationException exDBDis)
                //        {
                //            try
                //            {
                //                SqlCacheDependencyAdmin.EnableNotifications(((SqlDb)db).provider.connString);
                //            }
                //            catch (UnauthorizedAccessException exPerm)
                //            {
                //                throw exPerm;
                //            }
                //        }
                //        catch (TableNotEnabledForNotificationException exTabDis)
                //        {
                //            try
                //            {
                //                SqlCacheDependencyAdmin.EnableTableForNotifications(((SqlDb)db).provider.connString, this.name);
                //            }
                //            catch (SqlException exc)
                //            {
                //                throw exc;
                //            }
                //        }
                //        finally
                //        {
                //            reader = cmd.ExecuteReader();
                //            FillByIndex(reader, list);
                //            ORMCache.Instance.Insert(cmd.CommandText.GetHashCode().ToString(), list, SqlDep);
                //        }
                //    }
                //    else
                //    {
                //        list = ORMCache.Instance.Get(cmd.CommandText.GetHashCode().ToString()) as IList;
                //    }
                //}

                #endregion
            }
			finally
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
						reader.Close();
					reader.Dispose();
					reader = null;
				}
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}
			}
			FireTriggers(list, new TriggerEventArgs(db, Timing.AfterActivate));
		}

        public virtual void Select(IDb db, string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction, CommandType commType, IList list)
        {
            ErrorIfSPResult();
            IDbConnection conn = db.Connection;
            if (transaction != null)
                conn = transaction.Connection;

            IDbCommand cmd = null;
            IDataReader reader = null;

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = commType;
                cmd.CommandTimeout = conn.ConnectionTimeout;
                db.AddParameters(cmd, parameterName, parameters, transaction);

                TraceObject.Instance.TraceCommand(cmd);

                reader = cmd.ExecuteReader();
                FillByName(reader, list);
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    reader.Dispose();
                    reader = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            FireTriggers(list, new TriggerEventArgs(db, Timing.AfterActivate));
        }

        public virtual void Select(IDb db, string procName, string[] parameterName, object[] parameters, string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, out object[] outParameterResult, IList list)
        {
            ErrorIfSPResult();

            IDbConnection conn = db.Connection;
            IDbTransaction transaction = db.Transaction;
            if (transaction != null)
                conn = transaction.Connection;

            SqlResultSet rs = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                int index = 0;
                outParameterResult = new object[outputParameterName.Length];

                cmd = conn.CreateCommand();
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                db.AddParameters(cmd, parameterName, parameters, transaction);
                IDbDataParameter[] outputParams = db.AddOutputReturnParameters(cmd, outputParameterName,
                        outputParameterSize, outputParameterType, outputParameterDirection, transaction);

                TraceObject.Instance.TraceCommand(cmd);
                
                reader = cmd.ExecuteReader();
                FillByName(reader, list);

                if (reader != null)
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    reader.Dispose();
                    reader = null;
                }

                for (int i = 0; i < outputParams.Length; i++)
                {
                    outParameterResult[index] = outputParams[i].Value;
                    index++;
                }
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    reader.Dispose();
                    reader = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            FireTriggers(list, new TriggerEventArgs(db, Timing.AfterActivate));
        }
		
		public virtual object Find(IDb session, object key)
		{
			ErrorIfSPResult();
			SqlColumn pk = null;
			object result = null;
			foreach (SqlColumn c in columns)
			{
				if (c.IsPK)
				{
					pk = c;
					break;
				}
			}
			if (pk != null)
			{
				SqlQuery q = new SqlQuery();
				q.Constrain(pk.Alias).Equal(key);
				IList list = new ArrayList(1);
				Select(session, q, list);
				if (list.Count > 0)
					result = list[0];
			}
			return result;
		}
		
		public virtual void Exec(IDb db, string procName, object[] parameters, IList list)
		{
			int len = (parameters == null) ? 0 : parameters.Length;
			string sql = GetExecSql(procName, len);
			IDbCommand cmd = null;
			IDataReader reader = null;
			try
			{
				cmd = db.Connection.CreateCommand();
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = db.Transaction;
				for (int i = 0; i < len; i++)
				{
					int j = i + 1;
					IDbDataParameter p = cmd.CreateParameter();
					p.ParameterName = string.Format("@{0}", j);
					SqlUtils.PrepParam(p, parameters[i]);
					cmd.Parameters.Add(p);
				}

				TraceObject.Instance.TraceCommand(cmd);
				
				reader = cmd.ExecuteReader();
				FillByName(reader, list);
			}
			finally
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
						reader.Close();
					reader.Dispose();
					reader = null;
				}
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}
			}
			FireTriggers(list, new TriggerEventArgs(db, Timing.AfterActivate));
		}

        public virtual void Exec(IDb db, string procName, string[] parameterName, object[] parameters, IList list)
        {
            int len = (parameters == null) ? 0 : parameters.Length;
            string sql = GetExecSql(procName);
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                cmd = db.Connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = db.Transaction;
                
                db.AddParameters(cmd, parameterName, parameters, db.Transaction);

                TraceObject.Instance.TraceCommand(cmd);

                reader = cmd.ExecuteReader();
                FillByName(reader, list);
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    reader.Dispose();
                    reader = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            FireTriggers(list, new TriggerEventArgs(db, Timing.AfterActivate));
        }
		
		protected virtual string GetInsertSql()
		{
			if (insertSQL == null)
			{
				StringBuilder sql = new StringBuilder("insert into ");
				StringBuilder values = new StringBuilder();
				if (schema != null && schema.Length > 0)
					sql.Append("[").Append(schema).Append("].");
				sql.Append("[").Append(name).Append("] (");
				bool comma = false;
				foreach (SqlColumn column in columns)
				{
					if (column.IsReadable && !column.IsID)
					{
						if (comma)
						{
							sql.Append(",");
							values.Append(",");
						}
						else
							comma = true;
						sql.Append("[").Append(column.Name).Append("]");
						values.Append("@").Append(column.Ordinal);
					}
				}
				sql.Append(") values (").Append(values.ToString()).Append(");");
				if (identity != null)
					sql.Append("set @id=scope_identity();");
				insertSQL = sql.ToString();
			}
			return insertSQL;
		}
		
		protected virtual string GetUpdateSql()
		{
			if (updateSQL == null)
			{
				StringBuilder sql = new StringBuilder("update ");
				if (schema != null && schema.Length > 0)
					sql.Append("[").Append(schema).Append("].");
				sql.Append("[").Append(name).Append("] set ");
				bool comma = false;
				foreach (SqlColumn column in columns)
				{
					if (column.IsReadable && !column.IsPK && !column.IsID)
					{
						if (comma)
							sql.Append(",");
						else
							comma = true;
						sql.Append("[").Append(column.Name).Append("]=@").Append(column.Ordinal);
					}
				}
				sql.Append(" where ");
				comma = false;
				foreach (SqlColumn column in columns)
				{
					if (column.IsReadable && column.IsPK)
					{
						if (comma)
							sql.Append(" and ");
						sql.Append("[").Append(column.Name).Append("]=@pk").Append(column.Ordinal);
					}
				}
				sql.Append(";");
				updateSQL = sql.ToString();
			}
			return updateSQL;
		}
		
		protected virtual string GetDeleteSql()
		{
			if (deleteSQL == null)
			{
				StringBuilder sql = new StringBuilder("delete from ");
				if (schema != null && schema.Length > 0)
					sql.Append("[").Append(schema).Append("].");
				sql.Append("[").Append(name).Append("] where 1=1");
				foreach (SqlColumn column in columns)
				{
					if (column.IsPK)
						sql.Append(" and [").Append(column.Name).Append("]=@pk").Append(column.Ordinal);
				}
				sql.Append(";");
				deleteSQL = sql.ToString();
			}
			return deleteSQL;
		}
		
		protected virtual string GetDeleteSql(SqlQuery query)
		{
			StringBuilder buf = new StringBuilder("delete from ");
			if (schema != null && schema.Length > 0)
				buf.Append("[").Append(schema).Append("].");
			buf.Append("[").Append(name).Append("]");
			if (query != null)
				buf.Append(" ").Append(query.GetSql(this));
			buf.Append(";");
			return buf.ToString();
		}
		
		protected virtual string GetSelectSql(SqlQuery query)
		{
			if (selectSQL == null)
			{
				StringBuilder buf = new StringBuilder("select ");
				bool comma = false;
				foreach (SqlColumn column in columns)
				{
					if (comma)
						buf.Append(",");
					else
						comma = true;
					buf.Append("[").Append(column.Name).Append("]");
				}
				buf.Append(" from ");
				if (schema != null && schema.Length > 0)
					buf.Append("[").Append(schema).Append("].");
				buf.Append("[").Append(name).Append("]");
				selectSQL = buf.ToString();
			}
			if (query != null)
			{
				StringBuilder buf = new StringBuilder(selectSQL);
				buf.Append(" ").Append(query.GetSql(this));
				buf.Append(";");
				return buf.ToString();
			}
			else
				return string.Format("{0};", selectSQL);
		}

        protected virtual string GetExecSql(string proc)
        {
            return proc;
        }

		protected virtual string GetExecSql(string proc, int nParams)
		{
			StringBuilder buf = new StringBuilder("exec ");
			buf.Append(proc);
			if (nParams > 0)
				buf.Append(" ");
			for (int i = 0; i < nParams; i++)
			{
				int j = i + 1;
				if (i > 0)
					buf.Append(",");
				buf.Append("@").Append(j);
			}
			buf.Append(";");
			return buf.ToString();
		}
		
		private void FillByIndex(IDataReader reader, IList list)
		{
			while (reader.Read())
			{
				object item = Activator.CreateInstance(type, true);
				int i = 0;
				foreach (SqlColumn column in columns)
				{
					object val = reader[i++];
					column.SetValue(item, val);
				}
				list.Add(item);
			}
		}
		
		private void FillByName(IDataReader reader, IList list)
		{
			while (reader.Read())
			{
				object item = Activator.CreateInstance(type, true);
				foreach (SqlColumn column in columns)
				{
					object val = reader[column.Name];
					column.SetValue(item, val);
				}
				list.Add(item);
			}
		}
	}
}
