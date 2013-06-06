using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
namespace CN100.EnterprisePlatform.Wcf.Core
{
    /// <summary>
    /// WCF连接创建工厂
    /// </summary>
    public class WcfClientFactory
    {
        const string IP_TAG = "SERVER_IPADDRESS";

        const string PORT_TAG = "SERVER_PORT";

        const string DEFAULT_CONFIG = "wcfdefault";

        private static WcfClientFactory mDefault = null;

        static WcfClientFactory()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static log4net.ILog Log
        {
            get
            {
                return log;
            }
        }

        /// <summary>
        /// 默认
        /// </summary>
        public static WcfClientFactory Default
        {
            get
            {
                lock (typeof(WcfClientFactory))
                {
                    if (mDefault == null)
                        mDefault = new WcfClientFactory("wcfdefault");
                    return mDefault;
                }
            }
        }

        /// <summary>
        /// 初始化信息
        /// 监听地址默认是localhost
        /// 监听端口默认是9999
        /// 连接池数默认10个
        /// 构建一个默认的NetTcpBinding
        /// </summary>
        public WcfClientFactory()
        {
            string Address = "localhost";
            int Port = 9999;
            DefaultConnections = 10;
            Binding = new NetTcpBinding();
            Binding.CloseTimeout = new TimeSpan(0, 0, 1);
            Binding.Security = new NetTcpSecurity { Mode = SecurityMode.None };
            Binding.MaxReceivedMessageSize = 1024 * 1024 * 10;
            Binding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas { MaxStringContentLength = 1024 * 1024 * 10 };
            string value = System.Configuration.ConfigurationManager.AppSettings[IP_TAG];
            if (!string.IsNullOrEmpty(value))
                Address = value;
            value = System.Configuration.ConfigurationManager.AppSettings[PORT_TAG];
            int port = 9999;
            if (int.TryParse(value, out port))
                Port = port;
            Connections.Add(new ConnectItem { IPAddress = Address, Port = Port });

        }


        public WcfClientFactory(string name)
        {
            DefaultConnections = 10;
            Binding = new NetTcpBinding();
            Binding.CloseTimeout = new TimeSpan(0, 0, 1);
            Binding.Security = new NetTcpSecurity { Mode = SecurityMode.None };
            Binding.MaxReceivedMessageSize = 1024 * 1024 * 10;
            Binding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas { MaxStringContentLength = 1024 * 1024 * 10 };
            ClientFactorySelection selecton = (ClientFactorySelection)System.Configuration.ConfigurationManager.GetSection(name);
            if (selecton == null)
                throw CN100WcfException.CONFIG_NOTFOUND(name);
            foreach (ConnectionElement item in selecton.Connections)
            {
                Connections.Add(new ConnectItem { IPAddress = item.IP, Port = int.Parse(item.Port) });
            }
        }

        private const string BASEADDRESS = "net.tcp://{0}:{1}/{2}";

        private Dictionary<Type, IWcfTcpClientPool> mClientPools = new Dictionary<Type, IWcfTcpClientPool>(64);

        /// <summary>
        /// 获取或设置client默认的连接池数
        /// </summary>
        public int DefaultConnections
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置对应的NetTcpBinding信息
        /// </summary>
        public NetTcpBinding Binding
        {
            get;
            set;
        }

        private List<ConnectItem> mConnections = new List<ConnectItem>();

        public List<ConnectItem> Connections
        {
            get
            {
                return mConnections;
            }
        }


        /// <summary>
        /// 获取一个服务连接，使用方式始下
        /// <example>
        /// <code>
        ///  using (WcfTcpClient<IUserService> Client = WcfClientFactory.CreateClient<IUserService>())
        ///  {
        ///          User user = Client.Channel.GetUser("henry");
        ///  }
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="T">服务接口</typeparam>
        /// <returns>WcfTcpClient<T> </returns>
        public WcfTcpClient<T> CreateClient<T>()
        {
            IWcfTcpClientPool pool = null;
            lock (mClientPools)
            {
                Type type = typeof(T);
                if (!mClientPools.TryGetValue(type, out pool))
                {
                    ServiceContractAttribute[] scs = (ServiceContractAttribute[])type.GetCustomAttributes(typeof(ServiceContractAttribute), false);
                    if (scs == null || scs.Length == 0)
                        throw CN100WcfException.SERVER_CONTRACT_NOFOUND();
                    string name;
                    if (string.IsNullOrEmpty(scs[0].Name))
                        name = type.Name;
                    else
                        name = scs[0].Name;
                  
                    string[] address = new string[Connections.Count];
                    for (int i = 0; i < Connections.Count; i++)
                    {
                        address[i] = string.Format(BASEADDRESS, Connections[i].IPAddress, Connections[i].Port, name);
                    }
                    pool = new WcfTcpClientPool<T>(Binding, DefaultConnections, address);
                    mClientPools.Add(type, pool);
                }
            }
            WcfTcpClient<T> client = (WcfTcpClient<T>)pool.GetClient();
            if (client == null)
                throw new Exception("Connection is not available!");
            TimeSpan dt = DateTime.Now - client.ActiveTime;
            while (dt.TotalMinutes > 5)
            {
                client = (WcfTcpClient<T>)pool.GetClient();
                if (client == null)
                    throw new Exception("Connection is not available!");
                dt = DateTime.Now - client.ActiveTime;

            }
           
            client.ActiveTime = DateTime.Now;
            return client;
        }
    }
}
