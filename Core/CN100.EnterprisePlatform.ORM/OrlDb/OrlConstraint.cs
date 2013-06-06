using System.Data;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class OrlConstraint : IConstraint
	{
		private string column;
		private string oper;
		private object val;
		private OrlQuery query;
		protected int index;
		
		public OrlConstraint(string column, string op, object val)
		{
			this.column = column.ToLower();
			this.oper = op;
			this.val = val;
		}

        public OrlConstraint(IQuery query)
		{
			this.query = (OrlQuery) query;
		}
		
		public string Column
		{
			get { return column; }
		}
		
		public string Operator
		{
			get { return oper; }
		}
		
		public object Value
		{
			get { return val; }
			set { val = value; }
		}
		
		public bool HasQuery
		{
			get { return query != null; }
		}
		
		public IQuery Query
		{
			get { return query; }
		}
		
		public virtual string GetSql(OrlTable table, ref int offset)
		{
			if (HasQuery)
			{
				string sql = query.GetSql(table, ref offset);
				return string.Format("({0})", sql);
			}
			else
			{
				index = offset;
				offset = offset + 1;
				string name = table.Translate(column);
				if (name == null || name.Length == 0)
					//name = column;
					throw new LightException(string.Format("column {0} not found in table {1}", column, table.Name));
				return string.Format("[{0}]{1}:{2}", name, oper, index);
			}
		}
		
		public virtual void SetParameters(IDbCommand cmd)
		{
			if (HasQuery)
				query.SetParameters(cmd);
			else
			{
				string pn = string.Format(":{0}", index);
				IDbDataParameter p = cmd.CreateParameter();
				p.ParameterName = pn;
				SqlUtils.PrepParam(p, val);
				cmd.Parameters.Add(p);
			}
		}
	}
}
