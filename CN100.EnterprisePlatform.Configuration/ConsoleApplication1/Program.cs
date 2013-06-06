using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CN100.EnterprisePlatform.Configuration;
using System.Configuration;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseWrite();
            WcfWrite();
            DomainWrite();
            ImageWrite();
            MQWrite();
            StyleWrite();
            SearchWrite();
            EmailWrite();
            MemcachedWrite();
            Console.ReadLine();
        }
        private static void BaseWrite()
        {
            Console.WriteLine("+++++++++++++++BaseInfo++++++++++++++");
            Console.WriteLine(BaseSection.Instance.Base.GetItemByKey("title").Value);
            //Console.WriteLine(BaseSection.Instance.Base.GetItemByKey("hotline").Value);
        }
        private static void MemcachedWrite()
        {
            Console.WriteLine("+++++++++++++++Memcached++++++++++++++");
            Console.WriteLine(Utils.MemcachedConfigHelper.ServiceName);
            Console.WriteLine(MemcachedSection.Instance.Hosts.ServiceName);
            Console.WriteLine(MemcachedSection.Instance.Hosts.GetItemByKey("Cache1").Name);
            Console.WriteLine(MemcachedSection.Instance.Hosts.GetItemByKey("Cache1").Ip);
            Console.WriteLine(MemcachedSection.Instance.Hosts.GetItemByKey("Cache1").Port);
        }

        private static void EmailWrite()
        {
            Console.WriteLine("+++++++++++++++Email++++++++++++++");
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);

            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);

            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);

            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);

            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.EmailConfigHelper.ServerName);
            Console.WriteLine(Utils.EmailConfigHelper.Address);
            Console.WriteLine(Utils.EmailConfigHelper.LoginAccount);
            Console.WriteLine(Utils.EmailConfigHelper.Password);
        }

        private static void SearchWrite()
        {
            Console.WriteLine("+++++++++++++++Search++++++++++++++");
            Console.WriteLine(Utils.SearchConfigHelper.ProductDirectory);
            Console.WriteLine(Utils.SearchConfigHelper.ProductPath);
            Console.WriteLine(Utils.SearchConfigHelper.JobTime);
            Console.WriteLine(Utils.SearchConfigHelper.DictPath);
        }

        private static void StyleWrite()
        {
            Console.WriteLine("+++++++++++++++Style++++++++++++++");
            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
            System.Threading.Thread.Sleep(100);

            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
            System.Threading.Thread.Sleep(100);

            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
            System.Threading.Thread.Sleep(100);

            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
            Console.WriteLine(Utils.StyleDomainHelper.StyleDomain);
        }

        private static void MQWrite()
        {
            Console.WriteLine("+++++++++++++++MQ+++++++++++++++++");
            Console.WriteLine(Utils.MQConfigHelper.MQQualityHelper.ServiceName);
        }

        private static void ImageWrite()
        {
            Console.WriteLine("+++++++++++++++Image+++++++++++++++++");
            Console.WriteLine(Utils.ImageDomainHelper.ImageDomain);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.ImageDomainHelper.ImageDomain);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.ImageDomainHelper.ImageDomain);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.ImageDomainHelper.ImageDomain);
            System.Threading.Thread.Sleep(100);
            Console.WriteLine(Utils.ImageDomainHelper.ImageDomain);
        }

        private static void DomainWrite()
        {
            Console.WriteLine("+++++++++++++++Domain+++++++++++++++++");
            Console.WriteLine(Utils.DomainHelper.PayDomain);
            Console.WriteLine(Utils.DomainHelper.ProductDomain);
        }

        private static void WcfWrite()
        {
            Console.WriteLine("++++++++++++++++Wcf++++++++++++++++");
            foreach (WcfItemsElementCollection item in WcfSection.Instance.Hosts)
            {

            }
            Console.WriteLine(string.Format("Name:{0}", WcfSection.Instance.Hosts.Items.Name));
            Console.WriteLine(string.Format("IP:{0}", WcfSection.Instance.Hosts.GetItemByKey("test").GetItemAt(0).Ip));
            Console.WriteLine(string.Format("Port:{0}", WcfSection.Instance.Hosts.GetItemByKey("test").GetItemAt(0).Port.ToString()));
            Console.WriteLine(string.Format("IsDefault:{0}", WcfSection.Instance.Hosts.GetItemByKey("test").GetItemAt(0).IsDefault.ToString()));
            Console.WriteLine(string.Format("Count:{0}", WcfSection.Instance.Hosts.Count.ToString()));
        }
    }
}
