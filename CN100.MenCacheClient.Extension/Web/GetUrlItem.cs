﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
namespace CN100.MenCacheClient.Extension.Web
{
    public class GetUrlItem:DespatchItem
    {
        
        private HttpWebRequest mRequest;

        private HttpWebResponse mResponse;

        public GetUrlItem()
        {
            Coding = Encoding.UTF8;
        }

        public System.Web.Caching.Cache WebCache
        {
            get;
            set;
        }

        public Encoding Coding
        {
            get;
            set;
        }

        public CacheManager Cache
        {
            get;
            set;
        }

        private DateTime? Expiry
        {
            get;
            set;
        }

        public MenCacheModule HttpModule
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string URL
        {
            get;
            set;
        }

        public override void Execute()
        {
            lock (typeof(GetUrlItem))
            {
                PageCacheItem page = new PageCacheItem();
                mRequest = (HttpWebRequest)WebRequest.Create(URL);
                mResponse = (HttpWebResponse)mRequest.GetResponse();
                int length=0;
                foreach(string key in mResponse.Headers.Keys)
                {
                    page.Headers.Add(new HeaderItem{ Name=key, Value= mResponse.Headers[key]});
                    if (key == "Content-Length")
                    {
                        int.TryParse(mResponse.Headers[key], out length);
                    }
                }
                page.Data = new Byte[length];
                int offset=0;
                int count;
                using (System.IO.Stream stream = mResponse.GetResponseStream())
                {
                    count= stream.Read(page.Data, offset, page.Data.Length-offset);
                    offset += count;
                    while (count > 0)
                    {
                        count = stream.Read(page.Data, offset, page.Data.Length - offset);
                        offset += count;
                    }
                }
                
                if (Cache != null && HttpModule.MenCacheEnabled)
                    Cache.Set(Key, page);
                if (WebCache != null && HttpModule.WebCacheEnabled)
                {
                    WebCache.Remove(Key);
                    WebCache.Add(Key, page, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
           
            
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            
            if (mResponse != null)
                mResponse.Close();
        }
    }
}
