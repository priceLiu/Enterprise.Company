using System.Data;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class SqlDbAdapter : SqlDb
	{
		public SqlDbAdapter(SqlProvider provider, IDbConnection cn)
			: base(provider, cn)
		{}
		
		protected virtual void CloseConnection()
		{}
	}
}
