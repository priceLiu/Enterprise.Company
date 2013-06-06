using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace CN100.MenCacheClient.Extension.Web
{
    public class MenCacheModule : System.Web.IHttpModule
    {
        private const string GENERATOR_TAG = "_cache_generator";

        private static Dictionary<string, string> mGenerator = new Dictionary<string, string>();

        internal bool MenCacheEnabled = false;

        internal bool WebCacheEnabled = false;

        private UrlMatch mUrlMatch;

        private string RootURL;

        private string MenCacheHost;

        private Despatch GetDespatch;

        private CacheManager MenCache;

        public void Dispose()
        {
           
        }
        
        public void Init(System.Web.HttpApplication context)
        {
          
            context.BeginRequest += Application_BeginRequest;
            context.EndRequest += Application_EndRequest;
            RootURL = System.Configuration.ConfigurationManager.AppSettings["ROOT_URL"];
            if (string.IsNullOrEmpty(RootURL))
                RootURL = "http://localhost";
            MenCacheHost = System.Configuration.ConfigurationManager.AppSettings["MEMCACHE_HOST"];
            if (string.IsNullOrEmpty(MenCacheHost))
                MenCacheHost = "127.0.0.1:11211";
            Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["ENABLED_MEMCACHE"], out MenCacheEnabled);
            Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["ENABLED_WEBCACHE"], out WebCacheEnabled);
            GetDespatch = new Despatch();
            try
            {
                if (MenCacheEnabled)
                    MenCache = new CacheManager(MenCacheHost);
               
            }
            catch
            {
            }
            
        }

        private void Cache_WithWeb(HttpContext context, HttpRequest request,string key)
        {
            PageCacheItem data = (PageCacheItem)context.Cache[key];
            if (data == null)
            {
                Generator_Cache(context,request, key);
            }
            else
            {
                OutputCache(data, context);
            }
        }

        private void Generator_Completed(IDespatchItem item)
        {
            lock (mGenerator)
            {
                GetUrlItem gui = (GetUrlItem)item;
                mGenerator.Remove(gui.Key);
            }
        }

        private void Generator_Cache(HttpContext context, HttpRequest request, string key)
        {
            if (request.QueryString[GENERATOR_TAG] == null)
            {
                lock (mGenerator)
                {
                    if (mGenerator.ContainsKey(key))
                        return;

                    GetUrlItem item = new GetUrlItem();
                    item.HttpModule = this;
                    item.Cache = MenCache;
                    item.WebCache = context.Cache;
                    item.URL = key;
                    item.Key = key;
                    if (request.QueryString.Count == 0)
                    {
                        item.URL += "?";
                    }
                    else
                    {
                        item.URL += "&";
                    }
                    item.URL += "_cache_generator=true";
                    item.Completed = Generator_Completed;
                    mGenerator.Add(key, key);
                    GetDespatch.Add(item);
                }
            }
        }

        private void Cache_MenCache(HttpContext context, HttpRequest request, string key)
        {
            PageCacheItem data;
            if (WebCacheEnabled)
            {
                data = (PageCacheItem)context.Cache[key];
                if (data != null)
                {
                    OutputCache(data, context);
                    return;
                }
            }
            data = (PageCacheItem)MenCache.Get(key);
            if (data != null)
            {
                if (WebCacheEnabled)
                {
                    context.Cache.Add(key, data, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Low, null);
                }
                OutputCache(data, context);
            }
            else
            {
                Generator_Cache(context, request, key);
            }

        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
          
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            HttpRequest request = context.Request;
            string key = context.Request.Url.ToString();
            LoadUrlMatch(request);
            if (WebCacheEnabled || MenCacheEnabled)
            {
                if (request.RequestType == "GET" && mUrlMatch.Match(request.FilePath))
                {
                    if (MenCacheEnabled && MenCache.Initialized)
                    {
                        Cache_MenCache(context, request, key);
                    }
                    else
                    {
                        if(WebCacheEnabled)
                            Cache_WithWeb(context, request, key);
                    }

                }
            }
        }

        private void OutputCache(PageCacheItem value, HttpContext context)
        {

            byte[] data = Encoding.ASCII.GetBytes("0\r\n\r\n");
            context.Response.BinaryWrite(value.Data);
            context.Response.BinaryWrite(data);
            context.Response.Flush();
            context.Response.End();
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {

        }

        private void LoadUrlMatch(HttpRequest request)
        {
            lock (this)
            {
                if (mUrlMatch==null)
                {
                    mUrlMatch = new UrlMatch();
                    mUrlMatch.LoadFile(request.PhysicalApplicationPath + "urlmatch.ini");
                }
            }
        }
    }
}
