using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
namespace CN100.EnterprisePlatform.Web.Core
{
    class CacheModule:System.Web.IHttpModule
    {

        private const string GENERATOR_TAG = "_cache_generator";
        
        private static Dictionary<string, string> mGenerator = new Dictionary<string, string>();

        internal bool MenCacheEnabled = false;

        internal bool WebCacheEnabled = false;

        private UrlMatch mUrlMatch;

        private Despatch GetDespatch;

        public void Dispose()
        {

        }

        public void Init(System.Web.HttpApplication context)
        {

            context.BeginRequest += Application_BeginRequest;
            context.EndRequest += Application_EndRequest;
          
            GetDespatch = new Despatch();
            

        }

        private void Cache_WithWeb(HttpContext context, HttpRequest request, string key, UrlItem urlitem)
        {
            PageCacheItem data = (PageCacheItem)context.Cache[key];
            if (data == null)
            {
                Generator_Cache(context, request, key,urlitem);
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

        private void Generator_Cache(HttpContext context, HttpRequest request, string key,UrlItem urlitem)
        {
            if (request.QueryString[GENERATOR_TAG] == null)
            {
                lock (mGenerator)
                {
                    if (mGenerator.ContainsKey(key))
                        return;

                    GetUrlItem item = new GetUrlItem();
                    item.Item = urlitem;
                    item.Cache = context.Cache;
                    item.URL = context.Request.Url.ToString();
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

        static string getMd5Hash(string input)
        {

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {

            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            HttpRequest request = context.Request;
            if (request.RequestType == "GET")
            {
                LoadUrlMatch(request);
                UrlItem urlitem = mUrlMatch.Match(request.FilePath);
                if (urlitem != null)
                {
                    string key =  getMd5Hash(context.Request.Url.ToString());
                    Cache_WithWeb(context, request, key, urlitem);
                }
            }

        }

        private byte[] eofdata = Encoding.ASCII.GetBytes("0\r\n\r\n");

        private void OutputCache(PageCacheItem value, HttpContext context)
        {

           
            context.Response.BinaryWrite(value.Data);
            context.Response.BinaryWrite(eofdata);
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
                if (mUrlMatch == null)
                {
                    mUrlMatch = new UrlMatch();
                    mUrlMatch.LoadFile(request.PhysicalApplicationPath + "urls.xml");
                }
            }
        }
    }
}
