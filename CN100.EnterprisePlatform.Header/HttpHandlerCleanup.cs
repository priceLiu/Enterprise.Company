using System;
using System.Web;

namespace CN100.EnterprisePlatform.Header
{
    public class HttpHandlerCleanup : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += new EventHandler(context_PreSendRequestHeaders);
        }

        void context_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Headers.Remove(ConstManager.SERVER);
            response.Headers.Remove(ConstManager.ETAG);
        }
    }
}
