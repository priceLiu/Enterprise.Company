using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core
{
    [Serializable]
    public class PageCacheItem
    {
        public PageCacheItem()
        {
            Headers = new List<HeaderItem>();
            Data = new byte[0];
        }

        public List<HeaderItem> Headers
        {
            get;
            set;
        }

        public byte[] Data
        {
            get;
            set;
        }
    }

    [Serializable]
    public class HeaderItem
    {
        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
