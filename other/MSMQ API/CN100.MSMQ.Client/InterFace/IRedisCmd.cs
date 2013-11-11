using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.MSMQ.API

{
    public interface IRedisCmd
    {
        void DeleteByKey(string key);
        void DeleteByKeySearch(string key);
    }
}
