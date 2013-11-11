using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using CN100.ProductDetail.Common;

namespace CN100.MSMQ.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            //参数列表
            Dictionary<string, string> paramArr = new Dictionary<string, string>();
            paramArr.Add("t", string.Empty);
            OptionSet opset = new OptionSet();
            foreach (KeyValuePair<string, string> item in paramArr)
                opset.Add(item.Key + "=", v => paramArr[item.Key] = v);
            opset.Parse(args);

            //服务方式运行
            if (paramArr["t"] == "winsvr")
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new RuntimeForWinsvr() };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Startup.Run();  //win32方式运行
                Console.ReadLine();
            }
        }
    }

    /// <summary>
    /// 从服务方式启动
    /// </summary>
    public partial class RuntimeForWinsvr : ServiceBase
    {
        public RuntimeForWinsvr()
        {
            this.ServiceName = "CN100.MSMQ.Hosting";
        }

        protected override void OnStart(string[] args)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Startup.Run));
            th.IsBackground = true;
            th.Start();
        }

        protected override void OnStop()
        {
            Environment.Exit(0);
        }
    }
}
