using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.Wcf.Core;
using CN100.EnterprisePlatform.Wcf.Core.Config;
namespace Wcf.Test.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            WcfClientFactory factory;
            factory = WcfClients.GetFactory("default");

        }
       
    }
}
