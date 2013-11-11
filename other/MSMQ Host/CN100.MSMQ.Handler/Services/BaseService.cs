using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.Wcf.Core;
using CN100.EnterprisePlatform.Wcf.Core.Config;
using System.Text.RegularExpressions;

namespace CN100.MSMQ.Handler.Services
{
    public class BaseService
    {
        protected Regex RegexFixSystemChar;

         

        protected WcfClientFactory factory;
        public BaseService()
        {
            RegexFixSystemChar = new Regex(@"(!\\)\\\\([rnt])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            factory = WcfClients.GetFactory("ProductDetail");
        }
        /// <summary>从Redis中删除一条数据
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key)
        {
            if (CN100.Redis.Client.RedisClientUtility.SearchKeyList(key).Contains(key))
            {
                CN100.Redis.Client.RedisClientUtility.Del(key);
            }
        }
    }
}
