using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.Wcf.Core;
using System.Configuration;
namespace Wcf.Test.Hosting
{
    class Program
    {
        static Random mRan = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine(typeof(Program).Assembly.GetName().Version);
           
           // TestPager();
            StartHosting();
            Console.ReadLine();
        }
        /// <summary>
        /// 
        /// </summary>
        static void StartHosting()
        {

            string path = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);
           
            WcfTcpServer server = new WcfTcpServer();
           
            foreach (string dll in System.IO.Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    server.LoadInterfaceAssembly(System.Reflection.Assembly.LoadFile(dll));
                }
                catch (Exception e_)
                {
                    Console.WriteLine(e_.Message);
                }
            }
            foreach (string dll in System.IO.Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    server.LoadImplAssembly(System.Reflection.Assembly.LoadFile(dll));
                }
                catch (Exception e_)
                {
                    Console.WriteLine(e_.Message);
                }
            }
            server.Start();
            System.Threading.Thread.Sleep(-1);

        }
        
    }
}
