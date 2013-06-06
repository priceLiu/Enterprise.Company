using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
namespace CN100.EnterprisePlatform.Web.Core
{
    class GetUrlItem : DespatchItem
    {

        private HttpWebRequest mRequest;

        private HttpWebResponse mResponse;

        public GetUrlItem()
        {
            Coding = Encoding.UTF8;
        }

        public System.Web.Caching.Cache Cache
        {
            get;
            set;
        }

        public Encoding Coding
        {
            get;
            set;
        }



        public UrlItem Item
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
                int length = 0;
                foreach (string key in mResponse.Headers.Keys)
                {
                    page.Headers.Add(new HeaderItem { Name = key, Value = mResponse.Headers[key] });
                    if (key == "Content-Length")
                    {
                        int.TryParse(mResponse.Headers[key], out length);
                    }
                }
                page.Data = new Byte[length];
                int offset = 0;
                int count;
                using (System.IO.Stream stream = mResponse.GetResponseStream())
                {
                    count = stream.Read(page.Data, offset, page.Data.Length - offset);
                    offset += count;
                    while (count > 0)
                    {
                        count = stream.Read(page.Data, offset, page.Data.Length - offset);
                        offset += count;
                    }
                }
                if (Cache != null)
                {
                    Cache.Remove(Key);
                    Cache.Add(Key, page, null, DateTime.Now.AddMinutes(Item.Expiry), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
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
