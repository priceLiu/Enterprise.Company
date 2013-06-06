using System.Data;

namespace CN100.EnterprisePlatform.ORM
{
	public interface IProvider
	{
		IDb OpenDb(IDbConnection connection);
		IDb OpenDb(string connectString);
		IDb OpenDb();
		
		//bool Excepts(IDbConnection cn);
		//bool Excepts(string connString);
	}
}
