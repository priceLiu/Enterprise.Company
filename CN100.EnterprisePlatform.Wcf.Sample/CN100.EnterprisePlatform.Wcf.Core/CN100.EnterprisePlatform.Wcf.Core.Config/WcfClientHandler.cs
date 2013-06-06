using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.Configuration;
namespace CN100.EnterprisePlatform.Wcf.Core.Config
{
    public class WcfClients
    {
        const string DEFAULT = "default";
        const string PRODUCT = "product";
        const string IMAGE = "image";
        const string PICSPACE = "picspace";
        const string XIU = "xiu";
        const string PROMOTION = "promotion";
        const string QUALITYCENTER = "qualitycenter";
        const string MEMBER = "member";

        private static WcfSection mSection = WcfSection.Instance;

        private static Dictionary<string, WcfClientFactory> mFactory = new Dictionary<string, WcfClientFactory>();

        public static WcfClientFactory GetFactory(string name)
        {
            lock (mFactory)
            {

                WcfClientFactory result;
                if (mFactory.TryGetValue(name, out result))
                    return result;
                //WcfItemsElementCollection item = mSection.Hosts.GetItemByKey(name);
                result = new WcfClientFactory();
                result.Connections.Clear();
                //foreach (WcfElement host in item)
                //{
                 
                //    result.Connections.Add(new ConnectItem { IPAddress = host.Ip, Port = host.Port });
                //}
                mFactory.Add(name, result);
                return result;
            }
        }

        public static WcfClientFactory QualityCenter
        {
            get
            {
                return GetFactory(QUALITYCENTER);
            }
        }

        public static WcfClientFactory Promotion
        {
            get
            {
                return GetFactory(PROMOTION);
            }
        }
        public WcfClientFactory Member
        {
            get
            {
                return GetFactory(MEMBER);
            }
        }
        public static WcfClientFactory Xiu
        {
            get
            {
                return GetFactory(XIU);
            }
        }

        public static WcfClientFactory Product
        {
            get
            {
                return GetFactory(PRODUCT);
            }
        }
        public static WcfClientFactory PicSpace
        {
            get
            {
                return GetFactory(PICSPACE);
            }
        }
        public static WcfClientFactory Image
        {
            get
            {
                return GetFactory(IMAGE);
            }
        }
        public static WcfClientFactory Default
        {
            get
            {
                return GetFactory(DEFAULT);
            }
        }
    }
}
