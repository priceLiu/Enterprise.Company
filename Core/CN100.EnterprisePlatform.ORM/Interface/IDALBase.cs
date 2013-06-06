using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.ORM.Interface
{
    public interface IDALBase
    {
        IDb Db { get; set; }
    }
}
