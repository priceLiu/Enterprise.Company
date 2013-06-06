using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.ORM.Interface;

namespace CN100.EnterprisePlatform.ORM
{
    public class DALBase : IDALBase
    {
        protected IDb db = null;

        public IDb Db
        {
            get
            {
                return db;
            }

            set
            {
                db = value;
            }
        }
    }
}
