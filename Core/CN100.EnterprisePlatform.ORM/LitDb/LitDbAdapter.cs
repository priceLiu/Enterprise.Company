using System.Data;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class LitDbAdapter : LitDb
	{
        public LitDbAdapter(LitProvider provider, IDbConnection cn)
			: base(provider, cn)
		{}
		
		protected virtual void CloseConnection()
		{}
	}
}
