using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class OrlProvider : IProvider
	{
		protected string connString;
		
		public OrlProvider(string connectionString)
		{
			connString = connectionString;
		}

        public OrlProvider(): this(null)
		{}

        public virtual IDb OpenDb(IDbConnection cn)
		{
			OrlDb db = new OrlDb(this, cn);
			db.IsAdapter = true;
			return db;
		}

        public virtual IDb OpenDb(string connectString)
		{
            IDbConnection cn = new Oracle.DataAccess.Client.OracleConnection(connectString);
			//cn.Open();
            IDb db = new OrlDb(this, cn);
			return db;
		}

        public virtual IDb OpenDb()
		{
            IDbConnection cn = new Oracle.DataAccess.Client.OracleConnection(connString);
			//cn.Open();
            IDb db = new OrlDb(this, cn);
			return db;
		}

        public virtual void OpenDb(OrlDb db)
        {
            if (db.IsClosed)
            {
                if (db.Connection == null)
                {
                    db.Connection = new Oracle.DataAccess.Client.OracleConnection(connString);
                    db.Connection.Open();
                }
                else
                {
                    if (db.Connection.State == ConnectionState.Closed)
                    {
                        db.Connection.Open();
                    }
                }
            }
        }

        public virtual void CloseDb(OrlDb db)
		{
			if (!db.IsClosed)
			{
				if (!db.IsAdapter && db.Connection != null)
				{
                    Oracle.DataAccess.Client.OracleConnection cn = (Oracle.DataAccess.Client.OracleConnection)db.Connection;
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn.Dispose();
					db.Connection = null;
				}
			}
		}
	}
}
