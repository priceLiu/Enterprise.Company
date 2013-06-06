using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.Common;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class LitDb : IDb
	{
        private readonly object objLock = new object();
		protected LitProvider provider;
		protected IDbConnection connection;

        private DbProviderFactory factory;

		protected IDbTransaction transaction;
		protected bool autocommit = true;
        protected bool autoclose = true;
		private bool adapter = false;
		private bool closed = true;

        private int intCommandTimeout = 30;
        string litProvider = "System.Data.SQLite";

        public LitDb(LitProvider provider, IDbConnection cn)
		{
			this.provider = provider;
			this.connection = cn;

            factory = DbProviderFactories.GetFactory(litProvider);
		}

        public IDbConnection Connection
        {
            get { return connection; }

            set
            {
                connection = value;
            }
        }
		
		public IDbTransaction Transaction
		{
			get { return transaction; }
		}
		
		public bool IsAutoCommit
		{
			get { return autocommit; }
            set { autocommit = value; }
		}

        public bool IsAutoClose
        {
            get { return autoclose; }
            set { autoclose = value; }
        }
		
		public bool IsAdapter
		{
			get { return adapter; }
			set { adapter = value; }
		}
		
		public virtual void Dispose()
		{
			if (!IsClosed)
			{
				RollbackInternal();
				provider.CloseDb(this);
				IsClosed = true;
			}
		}
		
		public bool IsClosed
		{
			get { return closed; }
			set { closed = value; }
		}

        private void ErrorIfClosed()
        {
            //if (IsClosed)
            //    throw new LightException("db is closed");

            if (IsClosed && (transaction != null))
                throw new LightException("db is closed");
        }

        private void InternalOpen()
        {
            if (IsClosed)
            {
                provider.OpenDb(this);
                IsClosed = false;
            }
        }

        private void InternalClose()
        {
            if (!IsClosed)
            {
                if ((transaction == null) && autoclose)
                {
                    provider.CloseDb(this);
                    IsClosed = true;
                }
            }
        }
		
		public virtual ITable GetTable(Type type)
		{
			return TableFor(type);
		}
		
		private LitTable TableFor(Type type)
		{
			return LitTableFactory.Instance.Build(type);
		}
		
		public virtual int Insert(Type type, ICollection items)
		{
			ErrorIfClosed();            
			if (items.Count == 0)
				return 0;
			int result = 0;
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                if (autocommit)
                    BeginInternal();
                result = table.Insert(this, items);
                if (autocommit)
                    CommitInternal();
            }
            catch (Exception)
            {
                if (autocommit)
                    RollbackInternal();
                throw;
            }
            finally
            {
                InternalClose();
            }
			return result;
		}
		
		public virtual int Insert<T>(ICollection<T> items)
		{
			return Insert(typeof(T), (ICollection) items);
		}
		
		public virtual int Insert(object item)
		{
			if (item == null)
				return 0;
			object[] items = new object[] { item };
			int result = Insert(item.GetType(), items);
			return result;
		}
		
		public virtual int Update(Type type, ICollection items)
		{
			ErrorIfClosed();            
			if (items.Count == 0)
				return 0;
			int result = 0;
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                if (autocommit)
                    BeginInternal();
                result = table.Update(this, items);
                if (autocommit)
                    CommitInternal();
            }
            catch (Exception)
            {
                if (autocommit)
                    RollbackInternal();
                throw;
            }
            finally
            {
                InternalClose();
            }
			return result;
		}
		
		public virtual int Update<T>(ICollection<T> items)
		{
			return Update(typeof(T), (ICollection) items);
		}
		
		public virtual int Update(object item)
		{
			if (item == null)
				return 0;
			object[] items = new object[] { item };
			int result = Update(item.GetType(), items);
			return result;
		}
		
		public virtual int Delete(Type type, ICollection items)
		{
			ErrorIfClosed();            
			if (items.Count == 0)
				return 0;
			int result = 0;
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                if (autocommit)
                    BeginInternal();
                result = table.Delete(this, items);
                if (autocommit)
                    CommitInternal();
            }
            catch (Exception)
            {
                if (autocommit)
                    RollbackInternal();
                throw;
            }
            finally
            {
                InternalClose();
            }
			return result;
		}
		
		public virtual int Delete<T>(ICollection<T> items)
		{
			return Delete(typeof(T), (ICollection) items);
		}
		
		public virtual int Delete(object item)
		{
			if (item == null)
				return 0;
			object[] items = new object[] { item };
			int result = Delete(item.GetType(), items);
			return result;
		}
		
		public virtual int Delete(Type type, IQuery query)
		{
			ErrorIfClosed();            
			int result = 0;
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                if (autocommit)
                    BeginInternal();
                result = table.Delete(this, query);
                if (autocommit)
                    CommitInternal();
            }
            catch (Exception)
            {
                if (autocommit)
                    RollbackInternal();
                throw;
            }
            finally
            {
                InternalClose();
            }
			return result;
		}
		
		public virtual int Delete<T>(IQuery query)
		{
			return Delete(typeof(T), query);
		}
		
		protected virtual void DoSelect(Type type, IQuery query, IList list)
		{
			ErrorIfClosed();
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                table.Select(this, query, list);
            }
            finally
            {
                InternalClose();
            }
		}

        protected virtual void DoSelect(Type type, string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction, CommandType commType, IList list)
        {
            ErrorIfClosed();
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                table.Select(this, sql, parameterName, parameters, transaction, commType, list);
            }
            finally
            {
                InternalClose();
            }
        }

        protected virtual void DoSelect(Type type, string procName, string[] parameterName, object[] parameters, string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, out object[] outParameterResult, IList list)
        {
            ErrorIfClosed();
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                table.Select(this, procName, parameterName, parameters, outputParameterName, outputParameterSize, outputParameterType, outputParameterDirection, out outParameterResult, list);
            }
            finally
            {
                InternalClose();
            }
        }
		
		public virtual IList Select(Type type, IQuery query)
		{
			IList list = new ArrayList();
			DoSelect(type, query, list);
			return list;
		}

        /// <summary>
        /// To get list from the database
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> Select<T>(string sql)
        {
            return this.Select<T>(sql, null, null, null, CommandType.Text);
        }

        /// <summary>
        /// Get list from the database with a prepared statement
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <param name="parameterName">The parameter names of the prepared statement</param>
        /// <param name="parameters">The parameters of the prepared statement</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> Select<T>(string sql, string[] parameterName, Object[] parameters)
        {
            return this.Select<T>(sql, parameterName, parameters, null, CommandType.Text);
        }

        /// <summary>
        /// To get list from the database with a transaction
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> Select<T>(string sql, IDbTransaction transaction)
        {
            return this.Select<T>(sql, null, null, transaction, CommandType.Text);
        }

        /// <summary>
        /// Get list from the database with a transaction and prepared statement
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <param name="parameterName">The parameter names of the prepared statement</param>
        /// <param name="parameters">The parameters of the prepared statement</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> Select<T>(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            return this.Select<T>(sql, parameterName, parameters, transaction, CommandType.Text);
        }

        /// <summary>
        /// To get list from the database by store procedure
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> SelectByStoreProc<T>(string storeProc)
        {
            return this.Select<T>(storeProc, null, null, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get list from the database by store procedure with parameters
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <param name="parameterName">The parameter names of the store procedure</param>
        /// <param name="parameters">The parameters of the store procedure</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> SelectByStoreProc<T>(string storeProc, string[] parameterName, Object[] parameters)
        {
            return this.Select<T>(storeProc, parameterName, parameters, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// To get list from the database by store procedure with a transaction
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> SelectByStoreProc<T>(string storeProc, IDbTransaction transaction)
        {
            return this.Select<T>(storeProc, null, null, transaction, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get list from the database with a transaction and store procedure with parameters
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <param name="parameterName">The parameter names of the store procedure</param>
        /// <param name="parameters">The parameters of the store procedure</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> SelectByStoreProc<T>(string storeProc, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            return this.Select<T>(storeProc, parameterName, parameters, transaction, CommandType.StoredProcedure);
        }

        /// <summary>
        /// To get list from database
        /// </summary>
        /// <param name="sql">Command want to run</param>
        /// <param name="parameterName">String array of parameters' name, null for no parameters</param>
        /// <param name="parameters">Object array of parameters, null for no parameters</param>
        /// <param name="transaction">Transaction used, null for no transaction</param>
        /// <param name="commType">CommandType of the command</param>
        /// <returns>A list of T</returns>
        public virtual IList<T> Select<T>(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction, CommandType commType)
        {
            IList<T> list = new List<T>();
            DoSelect(typeof(T), sql, parameterName, parameters, transaction, commType, (IList)list);
            return list;
        }
		
		public virtual IList<T> Select<T>(IQuery query)
		{
			IList<T> list = new List<T>();
			DoSelect(typeof(T), query, (IList) list);
			return list;
		}
		
		public virtual object Find(Type type, object key)
		{
			ErrorIfClosed();
            object objFind = null;
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);
                objFind = table.Find(this, key);
            }
            finally
            {
                InternalClose();
            }
			return objFind;
		}
		
		public virtual T Find<T>(object key)
		{
			object result = Find(typeof(T), key);
			if (result != null)
				return (T) result;
			return default(T);
		}
		
		public virtual object Call(string procName, object[] parameters)
		{
			ErrorIfClosed();
			StringBuilder buf = new StringBuilder("exec @retval=").Append(procName);
			IDbCommand cmd = null;
			IDbDataParameter retval = null;
			try
			{
                InternalOpen();
				cmd = connection.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = transaction;
				retval = cmd.CreateParameter();
				retval.ParameterName = "@retval";
				retval.DbType = DbType.Object;
				SqlUtils.SetMaxValues(retval);
				retval.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(retval);
				SetupCommand(cmd, parameters, null, buf);
				buf.Append(";");
				cmd.CommandText = buf.ToString();
				cmd.Prepare();
				
				TraceObject.Instance.TraceCommand(cmd);
				
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (cmd != null)
				{
					cmd.Dispose();
					cmd = null;
				}

                InternalClose();
			}
			object val = retval.Value;
			if (DBNull.Value.Equals(val))
				return null;
			return val;
		}
		
		public virtual IList Exec(Type type, string procName, object[] parameters)
		{
			ErrorIfClosed();
            IList list = new ArrayList();
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);                
                table.Exec(this, procName, parameters, list);
            }
            finally
            {
                InternalClose();
            }
			return list;
		}

        public virtual IList Exec(Type type, string procName, string[] parameterName, object[] parameters)
        {
            ErrorIfClosed();
            IList list = new ArrayList();
            try
            {
                InternalOpen();
                LitTable table = TableFor(type);                
                table.Exec(this, procName, parameterName, parameters, list);
            }
            finally
            {
                InternalClose();
            }
            return list;
        }
		
		public IResultSet Exec(string procName, object[] parameters)
		{
			return Exec(procName, parameters, null);
		}
		
		public IResultSet Exec(string procName, object[] parameters, int[] outputs)
		{
			ErrorIfClosed();            
			SqlResultSet rs = null;
			IDbCommand cmd = null;
			IDataReader reader = null;
			StringBuilder buf = new StringBuilder("exec ").Append(procName);
			try
			{
                InternalOpen();
				int[] sortedOutputs = null;
				if (outputs != null)
					sortedOutputs = SortedCopy(outputs);
				
				cmd = connection.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.Transaction = transaction;
				IDbDataParameter[] outputParams = SetupCommand(cmd, parameters, sortedOutputs, buf);
                
				buf.Append(";");
				cmd.CommandText = buf.ToString();
				cmd.Prepare();
				
				TraceObject.Instance.TraceCommand(cmd);
				
				reader = cmd.ExecuteReader();
				
				// setup the result set
				int sz = 0;
				string[] names = null;
				DataTable meta = reader.GetSchemaTable();
				if (meta != null)
				{
					names = new string[meta.Rows.Count];
					foreach (DataRow datarow in meta.Rows)
					{
						// NOTE: this assumes that the name of the column
						// is in the first field of the schema table.
						// This is the case for .NET 2.0.
						names[sz++] = (string) datarow[0];
					}
					meta.Dispose();
					meta = null;
				}
				rs = new SqlResultSet(names);
				
				while (reader.Read())
				{
					object[] row = new object[sz];
					reader.GetValues(row);
					rs.AddRow(row);
				}
				// this is necessary to get output values
				reader.Close();
				
				if (sortedOutputs != null)
				{	
					for (int i = 0; i < sortedOutputs.Length; i++)
					{
						int index = sortedOutputs[i];
						object val = outputParams[i].Value;
						if (val == DBNull.Value)
							val = null;
						parameters[index] = val;
					}
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

                InternalClose();
			}
			return rs;
		}

        public IResultSet Exec(string procName, string[] parameterName, object[] parameters, string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, out object[] outParameterResult)
        {
            ErrorIfClosed();            
            SqlResultSet rs = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                InternalOpen();
                int index = 0;
                outParameterResult = new object[outputParameterName.Length];

                cmd = connection.CreateCommand();
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Transaction = transaction;
                this.AddParameters(cmd, parameterName, parameters, transaction);
                IDbDataParameter[] outputParams = this.AddOutputReturnParameters(cmd, outputParameterName,
                        outputParameterSize, outputParameterType, outputParameterDirection, transaction);
                
                TraceObject.Instance.TraceCommand(cmd);

                reader = cmd.ExecuteReader();

                // setup the result set
                int sz = 0;
                string[] names = null;
                DataTable meta = reader.GetSchemaTable();
                if (meta != null)
                {
                    names = new string[meta.Rows.Count];
                    foreach (DataRow datarow in meta.Rows)
                    {
                        // NOTE: this assumes that the name of the column
                        // is in the first field of the schema table.
                        // This is the case for .NET 2.0.
                        names[sz++] = (string)datarow[0];
                    }
                    meta.Dispose();
                    meta = null;
                }
                rs = new SqlResultSet(names);

                while (reader.Read())
                {
                    object[] row = new object[sz];
                    reader.GetValues(row);
                    rs.AddRow(row);
                }
                // this is necessary to get output values
                reader.Close();

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

                InternalClose();
            }
            return rs;
        }

        public virtual IList<T> Select<T>(string procName, string[] parameterName, object[] parameters, string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, out object[] outParameterResult)
        {
            IList<T> list = new List<T>();
            DoSelect(typeof(T), procName, parameterName, parameters, outputParameterName, outputParameterSize, outputParameterType,
            outputParameterDirection, out outParameterResult, (IList)list);
            return list;
        }

		private IDbDataParameter[] SetupCommand(IDbCommand cmd,
			object[] parameters, int[] outputs, StringBuilder buf)
		{
			int size = (outputs == null) ? 0 : outputs.Length;
			IDbDataParameter[] outputParams = new IDbDataParameter[size];
			int j = 0;
			if (parameters != null && parameters.Length > 0)
			{
				buf.Append(" ");
				for (int i = 0; i < parameters.Length; i++)
				{
					int x = i + 1;
					if (i > 0)
						buf.Append(",");
					buf.Append("@").Append(x);
					
					IDbDataParameter p = cmd.CreateParameter();
					p.ParameterName = string.Format("@{0}", x);
					SqlUtils.PrepParam(p, parameters[i]);
					if (outputs != null && Array.IndexOf<int>(outputs, i) > -1)
					{
						buf.Append(" output");
						p.Direction = ParameterDirection.InputOutput;
						outputParams[j++] = p;
					}
					cmd.Parameters.Add(p);
				}
			}
			return outputParams;
		}
		
		private int[] SortedCopy(int[] src)
		{
			int sz = src.Length;
			int[] dest = new int[sz];
			Array.Copy(src, 0, dest, 0, sz);
			Array.Sort<int>(dest);
			return dest;
		}
		
		protected virtual void BeginInternal()
		{
			if (transaction == null)
			{
                InternalOpen();
				TraceObject.Instance.TraceMessage("begin transaction;");
				transaction = connection.BeginTransaction();
			}
		}
		
		protected virtual void CommitInternal()
		{
			if (transaction != null)
			{
				TraceObject.Instance.TraceMessage("commit transaction;");
				transaction.Commit();
				transaction.Dispose();
				transaction = null;
                Dispose();
			}
		}
		
		protected virtual void RollbackInternal()
		{
			if (transaction != null)
			{
				TraceObject.Instance.TraceMessage("rollback transaction;");
				transaction.Rollback();
				transaction.Dispose();
				transaction = null;
                Dispose();
			}
		}
		
		public virtual void Begin()
		{
			BeginInternal();
			autocommit = false;
		}
		
		public virtual void Commit()
		{
			CommitInternal();
			autocommit = true;
		}
		
		public virtual void Rollback()
		{
			RollbackInternal();
			autocommit = true;
		}
		
		public IQuery Query()
		{
			return new SqlQuery();
        }

        #region DBUtil

        #region GetDateSet Interface
        /// <summary>
        /// To get DataSet from the database
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <param name="table">The table name</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSet(string sql, string table)
        {
            return this.GetDataSet(sql, table, null, null, null, CommandType.Text);
        }

        /// <summary>
        /// Get DataSet from the database with a prepared statement
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <param name="table">The table name</param>
        /// <param name="parameterName">The parameter names of the prepared statement</param>
        /// <param name="parameters">The parameters of the prepared statement</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSet(string sql, string table, string[] parameterName, Object[] parameters)
        {
            return this.GetDataSet(sql, table, parameterName, parameters, null, CommandType.Text);
        }

        /// <summary>
        /// To get DataSet from the database with a transaction
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <param name="table">The table name</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSet(string sql, string table, IDbTransaction transaction)
        {
            return this.GetDataSet(sql, table, null, null, transaction, CommandType.Text);
        }

        /// <summary>
        /// Get DataSet from the database with a transaction and prepared statement
        /// </summary>
        /// <param name="sql">The sql string</param>
        /// <param name="table">The table name</param>
        /// <param name="parameterName">The parameter names of the prepared statement</param>
        /// <param name="parameters">The parameters of the prepared statement</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSet(string sql, string table, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            return this.GetDataSet(sql, table, parameterName, parameters, transaction, CommandType.Text);
        }

        /// <summary>
        /// To get DataSet from the database by store procedure
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <param name="table">The table name</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSetByStoreProc(string storeProc, string table)
        {
            return this.GetDataSet(storeProc, table, null, null, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get DataSet from the database by store procedure with parameters
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <param name="table">The table name</param>
        /// <param name="parameterName">The parameter names of the store procedure</param>
        /// <param name="parameters">The parameters of the store procedure</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSetByStoreProc(string storeProc, string table, string[] parameterName, Object[] parameters)
        {
            return this.GetDataSet(storeProc, table, parameterName, parameters, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// To get DataSet from the database by store procedure with a transaction
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <param name="table">The table name</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSetByStoreProc(string storeProc, string table, IDbTransaction transaction)
        {
            return this.GetDataSet(storeProc, table, null, null, transaction, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get DataSet from the database with a transaction and store procedure with parameters
        /// </summary>
        /// <param name="storeProc">The store procedure</param>
        /// <param name="table">The table name</param>
        /// <param name="parameterName">The parameter names of the store procedure</param>
        /// <param name="parameters">The parameters of the store procedure</param>
        /// <returns>A DataSet</returns>
        public DataSet GetDataSetByStoreProc(string storeProc, string table, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            return this.GetDataSet(storeProc, table, parameterName, parameters, transaction, CommandType.StoredProcedure);
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// To execute a sql
        /// </summary>
        /// <param name="sql">The sql statement for execution</param>
        /// <returns>The number of row affected, the same as DbCommand.ExecuteNonQuery</returns>
        public int ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(sql, null, null);
        }

        /// <summary>
        /// To execute a prepared statement
        /// </summary>
        /// <param name="sql">The prepared statement</param>
        /// <param name="parameterName">The string array of parameter names</param>
        /// <param name="parameters">The object array of the parameters</param>
        /// <returns>The number of row affected, the same as DbCommand.ExecuteNonQuery</returns>
        public int ExecuteNonQuery(string sql, string[] parameterName, Object[] parameters)
        {
            int intRtn = 0;
            try
            {
                InternalOpen();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandTimeout = intCommandTimeout;
                    this.AddParameters(command, parameterName, parameters, null);

                    TraceObject.Instance.TraceCommand(command);

                    intRtn = command.ExecuteNonQuery();
                }
            }
            finally
            {
                InternalClose();
            }
            return intRtn;
        }

        /// <summary>
        /// To execute a sql with a transaction
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="transaction">The transaction used</param>
        /// <returns>The number of row affected, the same as DbCommand.ExecuteNonQuery</returns>
        public int ExecuteNonQuery(string sql, IDbTransaction transaction)
        {
            return this.ExecuteNonQuery(sql, null, null, transaction);
        }

        /// <summary>
        /// To execute a prepared statement with a transaction
        /// </summary>
        /// <param name="sql">The prepared statement</param>
        /// <param name="parameterName">The string array of the parameter names</param>
        /// <param name="parameters">The object array of the parameters</param>
        /// <param name="transaction">The transaction used</param>
        /// <returns>The number of row affected, the same as DbCommand.ExecuteNonQuery</returns>
        public int ExecuteNonQuery(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            int intRtn = 0;
            try
            {
                InternalOpen();
                using (IDbCommand command = transaction.Connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandTimeout = intCommandTimeout;
                    this.AddParameters(command, parameterName, parameters, transaction);

                    TraceObject.Instance.TraceCommand(command);

                    intRtn = command.ExecuteNonQuery();
                }
            }
            finally
            {
                InternalClose();
            }
            return intRtn;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// To execute scalar
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <returns>The result object of the statement</returns>
        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, null, null);
        }

        /// <summary>
        /// To execute scalar with a prepared statement
        /// </summary>
        /// <param name="sql">The prepared statement</param>
        /// <param name="parameterName">The string array of parameter names</param>
        /// <param name="parameters">The object array of the parameters</param>
        /// <returns>The result object of the prepared statement</returns>
        public object ExecuteScalar(string sql, string[] parameterName, Object[] parameters)
        {
            object objRtn;
            try
            {
                InternalOpen();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandTimeout = intCommandTimeout;
                    this.AddParameters(command, parameterName, parameters, null);

                    TraceObject.Instance.TraceCommand(command);

                    objRtn = command.ExecuteScalar();
                }
            }
            finally
            {
                InternalClose();
            }
            return objRtn;
        }

        /// <summary>
        /// To execute scalar with a transaction
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="transaction">The transaction used</param>
        /// <returns>The result object of the sql statement</returns>
        public object ExecuteScalar(string sql, IDbTransaction transaction)
        {
            return this.ExecuteScalar(sql, null, null, transaction);
        }

        /// <summary>
        /// To execute scalar with a transaction and parameters
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="parameterName">String array of parameters' name</param>
        /// <param name="parameters">Object array of parameters</param>
        /// <param name="transaction">The transaction used</param>
        /// <returns>The result object of the sql statement</returns>
        public object ExecuteScalar(string sql, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            object objRtn;
            try
            {
                InternalOpen();
                using (IDbCommand command = transaction.Connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandTimeout = intCommandTimeout;
                    this.AddParameters(command, parameterName, parameters, transaction);

                    TraceObject.Instance.TraceCommand(command);

                    objRtn = command.ExecuteScalar();
                }
            }
            finally
            {
                InternalClose();
            }
            return objRtn;
        }
        #endregion

        #region ExecuteStoreProcedure
        /// <summary>
        /// To execute store procedure
        /// </summary>
        /// <param name="storeProc">Store procedure</param>
        /// <param name="parameterName">String array of input parameter name</param>
        /// <param name="parameters">String array of input parameters value</param>
        /// <param name="outputParameterName">String array of output parameter name</param>
        /// <param name="outputParameterSize">Int array of output parameter size, null if no need to specify</param>
        /// <param name="outputParameterType">DbType array of output parameter type</param>
        /// <param name="outputParameterDirection">ParameterDirection array of parameter direction</param>
        /// <returns>Object array of those output parameters value with the sequence in the outputParameterName</returns>
        public object[] ExecuteStoreProc(string storeProc, string[] parameterName, Object[] parameters,
            string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection)
        {
            return ExecuteStoreProc(storeProc, parameterName, parameters, outputParameterName,
                outputParameterSize, outputParameterType, outputParameterDirection, null);
        }

        /// <summary>
        /// To execute store procedure with transaction
        /// </summary>
        /// <param name="storeProc">Store procedure</param>
        /// <param name="parameterName">String array of input parameter name</param>
        /// <param name="parameters">String array of input parameters value</param>
        /// <param name="outputParameterName">String array of output parameter name</param>
        /// <param name="outputParameterSize">Int array of output parameter size, null if no need to specify</param>
        /// <param name="outputParameterType">DbType array of output parameter type</param>
        /// <param name="outputParameterDirection">ParameterDirection array of parameter direction</param>
        /// <param name="transaction">Transaction used</param>
        /// <returns>Object array of those output parameters value with the sequence in the outputParameterName</returns>
        public object[] ExecuteStoreProc(string storeProc, string[] parameterName, Object[] parameters,
            string[] outputParameterName, int?[] outputParameterSize, DbType[] outputParameterType,
            ParameterDirection[] outputParameterDirection, IDbTransaction transaction)
        {
            object[] result = null;
            try
            {
                InternalOpen();
                int index = 0;
                result = new object[outputParameterName.Length];
                IDbConnection conn = connection;
                if (transaction != null)
                    conn = transaction.Connection;
                using (IDbCommand command = conn.CreateCommand())
                {
                    command.CommandText = storeProc;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = intCommandTimeout;
                    this.AddParameters(command, parameterName, parameters, transaction);
                    DbParameter[] output = this.AddOutputReturnParameters(command, outputParameterName,
                        outputParameterSize, outputParameterType, outputParameterDirection, transaction);

                    TraceObject.Instance.TraceCommand(command);

                    command.ExecuteNonQuery();
                    for (int i = 0; i < output.Length; i++)
                    {
                        result[index] = output[i].Value;
                        index++;
                    }
                }
            }
            finally
            {
                InternalClose();
            }
            return result;
        }
        #endregion

        #region ImplementFunction
        /// <summary>
        /// To get DataSet from database
        /// </summary>
        /// <param name="sql">Command want to run</param>
        /// <param name="table">Table name in the DataSet</param>
        /// <param name="parameterName">String array of parameters' name, null for no parameters</param>
        /// <param name="parameters">Object array of parameters, null for no parameters</param>
        /// <param name="transaction">Transaction used, null for no transaction</param>
        /// <param name="commType">CommandType of the command</param>
        /// <returns>DataSet of result</returns>
        public DataSet GetDataSet(string sql, string table, string[] parameterName, Object[] parameters, IDbTransaction transaction, CommandType commType)
        {
            DataSet set = new DataSet();
            try
            {
                InternalOpen();
                IDbConnection conn = connection;
                if (transaction != null)
                    conn = transaction.Connection;
                using (IDbCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commType;
                    command.CommandTimeout = intCommandTimeout;
                    this.AddParameters(command, parameterName, parameters, transaction);

                    TraceObject.Instance.TraceCommand(command);

                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = (DbCommand)command;
                    adapter.Fill(set, table);
                }
            }
            finally
            {
                InternalClose();
            }
            return set;
        }

        /// <summary>
        /// To add parameters and transaction to the Dbcommand
        /// </summary>
        /// <param name="command">Dbcommand</param>
        /// <param name="parameterName">String array of the parameters name</param>
        /// <param name="parameters">Object array of the parameters</param>
        /// <param name="transaction">The transaction you want to use</param>
        public void AddParameters(IDbCommand command, string[] parameterName, Object[] parameters, IDbTransaction transaction)
        {
            #region Set Transaction
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            else
            {
            }
            #endregion

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    DbParameter param = (DbParameter)command.CreateParameter();
                    param.ParameterName = parameterName[i];
                    param.Value = parameters[i] == null ? DBNull.Value : parameters[i];
                    command.Parameters.Add(param);
                }
            }
        }

        /// <summary>
        /// To add output parameters for a store procedure
        /// </summary>
        /// <param name="command">Store procedure</param>
        /// <param name="outputParameterName">String array of the output parameters' name</param>
        /// <param name="transaction">Transaction used, null for no transaction</param>
        /// <returns>Array of the output paramters</returns>
        public DbParameter[] AddOutputReturnParameters(IDbCommand command, string[] outputParameterName,
            int?[] outputParameterSize, DbType[] outputParameterType, ParameterDirection[] outputParameterDirection,
            IDbTransaction transaction)
        {
            #region Set Transaction
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            else
            {
            }
            #endregion

            if (outputParameterName != null)
            {
                DbParameter[] output = new DbParameter[outputParameterName.Length];
                for (int i = 0; i < outputParameterName.Length; i++)
                {
                    DbParameter param = (DbParameter)command.CreateParameter();
                    param.ParameterName = outputParameterName[i];
                    if (outputParameterSize[i] != null)
                        param.Size = (int)outputParameterSize[i];
                    param.DbType = outputParameterType[i];
                    param.Direction = outputParameterDirection[i];
                    command.Parameters.Add(param);
                    output[i] = param;
                }
                return output;
            }
            return new DbParameter[0];
        }
        #endregion

        #endregion
    }
}
