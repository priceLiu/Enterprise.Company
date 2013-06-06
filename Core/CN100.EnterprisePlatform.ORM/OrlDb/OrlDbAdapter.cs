using System.Data;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class OrlDbAdapter : OrlDb
	{
        public OrlDbAdapter(OrlProvider provider, IDbConnection cn)
			: base(provider, cn)
		{}
		
		protected virtual void CloseConnection()
		{}
	}
}
