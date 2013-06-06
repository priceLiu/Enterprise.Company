using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core
{
    public class WebContextModule:System.Web.IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(System.Web.HttpApplication context)
        {
            context.BeginRequest += Application_BeginRequest;
            context.EndRequest += Application_EndRequest;

        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            WebContext.Context = new WebContext();
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            WebContext.Context = null;
        }
    }
}
