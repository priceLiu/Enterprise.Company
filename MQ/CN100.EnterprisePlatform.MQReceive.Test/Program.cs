using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDS.Client;
using CN100.EnterprisePlatform.Configuration;

namespace CN100.EnterprisePlatform.MQReceive.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MDSClient mdsClient = new MDSClient(Utils.MQConfigHelper.MQQualityHelper.ServiceName,
                Utils.MQConfigHelper.MQQualityHelper.Ip, Utils.MQConfigHelper.MQQualityHelper.Port);

            mdsClient.MessageReceived += new MessageReceivedHandler(mdsClient_MessageReceived);

            mdsClient.Connect();

            Console.WriteLine("按回车键退出");
            Console.ReadLine();

            mdsClient.Disconnect();
        }

        static void mdsClient_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string messageText = Encoding.UTF8.GetString(e.Message.MessageData);

            Console.WriteLine();
            Console.WriteLine("收到消息 : " + messageText);
            Console.WriteLine("消息源    : " + e.Message.SourceApplicationName);
            Console.WriteLine("接收时间    : " + DateTime.Now.ToString());

            e.Message.Acknowledge();
        }
    }
}
