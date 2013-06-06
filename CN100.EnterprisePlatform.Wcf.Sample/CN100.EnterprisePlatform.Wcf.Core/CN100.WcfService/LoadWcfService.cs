/*********************************************************************
 * Copyright © CN100.COM 2012
 * File: LoadWcfService.cs
 * Time: 9/5/2012 1:14:05 PM
 * Author: Sharpish
 * URL:  http://www.cn100.com
 * Description:
 * History:
 * 
 * 
 *********************************************************************/

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using CN100.EnterprisePlatform.Wcf.Core;
using log4net;
using log4net.Config;

namespace CN100.WcfService
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LoadWcfService
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private WcfTcpServer _server;

        static LoadWcfService()
        {
            XmlConfigurator.Configure();
        }

        public void Start()
        {
            StartHosting();
        }

        public void Stop()
        {
            _server.Dispose();
        }

        private static void SetAppDomain(string name, string path)
        {
            //AppDomainSetup setup = new AppDomainSetup();
            //setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory+path;
            //setup.ShadowCopyFiles = "true";
            //setup.CachePath = Environment.CurrentDirectory;
            //string fileName = string.Format("{0}\\{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, path, "CN100.EnterprisePlatform.Wcf.Core.dll");
            //byte[] assemblyByte = File.ReadAllBytes(fileName);
            //System.Security.Policy.Evidence adevidence = AppDomain.CurrentDomain.Evidence;
            //AppDomain domain = AppDomain.CreateDomain(name, adevidence, setup);
            //Assembly assembly= domain.Load(assemblyByte);
            //string fullPath=AppDomain.CurrentDomain.BaseDirectory+path;
            //if (assembly!=null)
            //{
            //    Type type = assembly.GetType("CN100.EnterprisePlatform.Wcf.Core.WcfTcpServer");
            //     IWcfTcpServer server =(IWcfTcpServer)Activator.CreateInstance(type);
            //     foreach (string dll in System.IO.Directory.GetFiles(fullPath, "*.dll"))
            //     {
            //         try
            //         {
            //             server.LoadInterfaceAssembly(System.Reflection.Assembly.LoadFile(dll));
            //         }
            //         catch (Exception e_)
            //         {
            //             Console.WriteLine(e_.Message);
            //         }
            //     }
            //     foreach (string dll in System.IO.Directory.GetFiles(fullPath, "*.dll"))
            //     {
            //         try
            //         {
            //             server.LoadImplAssembly(System.Reflection.Assembly.LoadFile(dll));
            //         }
            //         catch (Exception e_)
            //         {
            //             Console.WriteLine(e_.Message);
            //         }
            //     }
            //     server.Start();
            //}
        }

        private void StartHosting()
        {
            string path = Path.GetDirectoryName(typeof (Program).Assembly.Location);

            _server = new WcfTcpServer(WcfServiceSection.Instance.Host.IP, WcfServiceSection.Instance.Host.Port);

            foreach (string fullName in from DllElement item in WcfServiceSection.Instance.Items select string.Format("{0}\\{1}", path, item.DllName))
            {
                try
                {
                    _server.LoadInterfaceAssembly(Assembly.LoadFile(fullName));
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            foreach (string fullName in from DllElement item in WcfServiceSection.Instance.Services select string.Format("{0}\\{1}", path, item.DllName))
            {
                try
                {
                    _server.LoadImplAssembly(Assembly.LoadFile(fullName));
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }


            _server.Start();
            Thread.Sleep(-1);
        }
    }
}