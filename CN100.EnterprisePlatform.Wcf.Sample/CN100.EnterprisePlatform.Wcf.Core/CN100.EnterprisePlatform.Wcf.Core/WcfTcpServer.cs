using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Description;
namespace CN100.EnterprisePlatform.Wcf.Core
{

    /// <summary>
    /// WcfTcp服务类
    /// </summary>
    public class WcfTcpServer:IDisposable
    {
        const string IP_TAG = "LISTEN_IPADDRESS";

        const string PORT_TAG = "LISTEN_PORT";

        const string IS_HTTP = "IS_HTTP";

        private bool mIsHttp = false;

        private bool mIsDisposed = false;

        private IList<ServiceDescription> mServices = new List<ServiceDescription>();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 构建服务类
        /// </summary>
        public WcfTcpServer()
        {
            Address = "loclahost";
            Port = 9999;

            string value = System.Configuration.ConfigurationManager.AppSettings[IP_TAG];
            if (!string.IsNullOrEmpty(value))
                Address = value;
            value = System.Configuration.ConfigurationManager.AppSettings[PORT_TAG];
            int port = 9999;
            if (int.TryParse(value, out port))
                Port = port;
            value = System.Configuration.ConfigurationManager.AppSettings[IS_HTTP];
            bool.TryParse(value, out mIsHttp);
            LoadBind();

        }

        private void LoadBind()
        {
            log4net.Config.XmlConfigurator.Configure();
            Binding = new NetTcpBinding();
            Binding.PortSharingEnabled = true;
            Binding.Security = new NetTcpSecurity { Mode = SecurityMode.None };
            Binding.MaxReceivedMessageSize = 1024 * 1024 * 10;
            Binding.ReaderQuotas = new XmlDictionaryReaderQuotas { MaxBytesPerRead = 1024 * 1024 * 10, MaxDepth = 64, MaxNameTableCharCount = 1024 * 1024 * 10, MaxArrayLength = 1024 * 1024 * 10, MaxStringContentLength = 1024 * 1024 * 10 };
            Binding.MaxBufferSize = 1024 * 1024 * 10;
        }

        public WcfTcpServer(string ip, int port)
        {
            Address = ip;
            Port = port;
            string value = System.Configuration.ConfigurationManager.AppSettings[IS_HTTP];
            bool.TryParse(value, out mIsHttp);
            LoadBind();
        }

        /// <summary>
        /// 获取或设置监听地址，默认是localhost
        /// </summary>
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置监听端口,默认是9999
        /// </summary>
        public int Port
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置一个NetTcpBinding信息
        /// </summary>
        public NetTcpBinding Binding
        {
            get;
            set;
        }


        /// <summary>
        /// 获取相关加载的服务描述
        /// </summary>
        public IList<ServiceDescription> Services
        {
            get
            {
                return mServices;
            }
        }

        /// <summary>
        /// 从程序集中加载所有服务描述
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public void LoadInterfaceAssembly(System.Reflection.Assembly assembly)
        {
            foreach (Type itype in assembly.GetTypes())
            {
                if (itype.IsInterface && itype.IsPublic)
                {
                    ServiceContractAttribute[] sc = (ServiceContractAttribute[])itype.GetCustomAttributes(typeof(ServiceContractAttribute), false);
                    if (sc.Length > 0)
                    {
                        BuildService(itype, sc[0]);
                    }
                }
            }
        }

        /// <summary>
        /// 从程序集中加载所有服务实现
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public void LoadImplAssembly(System.Reflection.Assembly assembly)
        {
            foreach (ServiceDescription sd in Services)
            {
                foreach (Type impltype in assembly.GetTypes())
                {
                    if (impltype.IsClass && impltype.IsPublic && impltype.GetInterface(sd.IType.FullName) != null)
                    {
                        sd.ImplType = impltype;
                        if (string.IsNullOrEmpty(sd.Name))
                        {
                            sd.Name = impltype.Name;
                        }
                    }
                }
            }
        }

        private void BuildService(Type type, ServiceContractAttribute sc)
        {
            ServiceDescription sd = new ServiceDescription();
            sd.IType = type;
            if (!string.IsNullOrEmpty(sc.Name))
            {
                sd.Name = sc.Name;
            }
            Services.Add(sd);
        }

        /// <summary>
        /// 启动服务,在执行些方法前请按顺序执行以下方法
        /// LoadInterfaceAssembly
        /// LoadImplAssembly
        /// </summary>
        public void Start()
        {
            string baseaddres = "net.tcp://{0}:{1}/{2}";
            string httpserviceaddress = "http://{0}:{1}/{2}";
            string hostaddress;
            foreach (ServiceDescription sd in Services)
            {
                try
                {
                    sd.Host = new ServiceHost(sd.ImplType);
#if DEBUG
                    ServiceDebugBehavior behavior =
       sd.Host.Description.Behaviors.Find<ServiceDebugBehavior>();

                    if (behavior != null)
                    {
                        behavior.IncludeExceptionDetailInFaults = true;

                    }
                    else
                    {
                        sd.Host.Description.Behaviors.Add(
                            new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                    }
#endif
                    if (mIsHttp)
                    {
                        ServiceMetadataBehavior smb = sd.Host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                        if (smb == null)
                        {
                            smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
                            smb.HttpGetUrl = new Uri(string.Format(httpserviceaddress, Address, 8080, sd.Name));
                            smb.HttpGetEnabled = true;
                            sd.Host.Description.Behaviors.Add(smb);

                        }
                        else
                        {
                            smb.HttpGetUrl = new Uri(string.Format(httpserviceaddress, Address, 8080, sd.Name));
                            smb.HttpGetEnabled = true;

                        }
                    }
                    hostaddress = string.Format(baseaddres, Address, Port, sd.Name);

                    sd.Host.AddServiceEndpoint(sd.IType, Binding, hostaddress);
                    Console.WriteLine("Service:\t{0} \r\nImplClass:\t{1} \r\nListen URL:\t{2}.", sd.IType, sd.ImplType, hostaddress);
                    log.InfoFormat("Service:\t{0} \r\nImplClass:\t{1} \r\nListen URL:\t{2}.", sd.IType, sd.ImplType, hostaddress); ;
                    if (mIsHttp)
                    {
                        BasicHttpBinding httpbinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                        httpbinding.Security.Mode = BasicHttpSecurityMode.None;
                        httpbinding.MaxReceivedMessageSize = 1024 * 1024 * 10;
                        httpbinding.ReaderQuotas = new XmlDictionaryReaderQuotas { MaxBytesPerRead = 1024 * 1024 * 10, MaxDepth = 64, MaxNameTableCharCount = 1024 * 1024 * 10, MaxArrayLength = 1024 * 1024 * 10, MaxStringContentLength = 1024 * 1024 * 10 };

                        hostaddress = string.Format(httpserviceaddress, Address, 8080, sd.Name);
                        sd.Host.AddServiceEndpoint(sd.IType, httpbinding, hostaddress);

                        Console.WriteLine("Service:\t{0} \r\nImplClass:\t{1} \r\nListen URL:\t{2}.", sd.IType, sd.ImplType, hostaddress);
                        log.InfoFormat("Service:\t{0} \r\nImplClass:\t{1} \r\nListen URL:\t{2}.", sd.IType, sd.ImplType, hostaddress);
                    }

                    sd.Host.Open();

                }
                catch (Exception e_)
                {
                    Console.WriteLine("Service :\t{0} \r\nImplClass:\t{1} \r\nError:\t{2}", sd.IType, sd.ImplType, e_.Message);
                    log.ErrorFormat("Service :\t{0} \r\nImplClass:\t{1} \r\nError:\t{2}", sd.IType, sd.ImplType, e_.Message);
                }
                Console.WriteLine("---------------------------------------------------");
            }
            Console.WriteLine("WcfTcpServer start...");
        }

        /// <summary>
        /// 释放wcf服务
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                if (!mIsDisposed)
                {
                    mIsDisposed = true;
                }
                foreach (ServiceDescription sd in Services)
                {
                    if (sd.Host != null)
                        sd.Host.Close();
                }
            }
        }
    }
}
