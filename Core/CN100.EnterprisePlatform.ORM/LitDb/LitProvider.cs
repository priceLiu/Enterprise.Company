using System.Data;
using System.Data.SQLite;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class LitProvider : IProvider
	{
		protected string connString;
		
		public LitProvider(string connectionString)
		{
			connString = connectionString;
		}

        public LitProvider(): this(null)
		{}

        public virtual IDb OpenDb(IDbConnection cn)
		{
			LitDb db = new LitDb(this, cn);
			db.IsAdapter = true;
			return db;
		}

        public virtual IDb OpenDb(string connectString)
		{
            IDbConnection cn = new SQLiteConnection(connectString);
			//cn.Open();
            IDb db = new LitDb(this, cn);
			return db;
		}

        public virtual IDb OpenDb()
		{
            IDbConnection cn = new SQLiteConnection(connString);
			//cn.Open();
            IDb db = new LitDb(this, cn);
			return db;
		}

        public virtual void OpenDb(LitDb db)
        {
            if (db.IsClosed)
            {
                if (db.Connection == null)
                {
                    db.Connection = new SQLiteConnection(connString);
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

        public virtual void CloseDb(LitDb db)
		{
			if (!db.IsClosed)
			{
				if (!db.IsAdapter && db.Connection != null)
				{
                    SQLiteConnection cn = (SQLiteConnection)db.Connection;
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn.Dispose();
					db.Connection = null;
				}
			}
		}
	}
}
