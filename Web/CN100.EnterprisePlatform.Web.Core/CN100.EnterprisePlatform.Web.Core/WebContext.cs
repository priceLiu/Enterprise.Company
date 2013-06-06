using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core
{
    public class WebContext
    {
        [ThreadStatic]
        private static WebContext mContext;
        public static WebContext Context
        {
            get
            {
                return mContext;
            }
            set
            {
                mContext = value;
            }
        }
        private KeyValueCollection<object> mProperties = new KeyValueCollection<object>();
        public object this[string name]
        {
            get
            {
                return mProperties[name];
            }
            set
            {
                mProperties[name] = value;
            }
            
        }
        public WebContext SetProperty(string name, object value)
        {
            this[name] = value;
            return this;
        }
    }

}
