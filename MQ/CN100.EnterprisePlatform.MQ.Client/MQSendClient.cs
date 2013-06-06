using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDS.Client;
using CN100.EnterprisePlatform.Configuration;
using MDS;

namespace CN100.EnterprisePlatform.MQ.Client
{
    public class MQSendClient
    {
        public string ServiceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }

        MDSClient client;
        public MQSendClient()
        {
            ServiceName = Utils.MQConfigHelper.MQQualityHelper.ServiceName;
            IpAddress = Utils.MQConfigHelper.MQQualityHelper.Ip;
            Port = Utils.MQConfigHelper.MQQualityHelper.Port;
            client = new MDSClient(ServiceName, IpAddress, Port);
        }

        public MQSendClient(string serviceName)
        {
            ServiceName = serviceName;
        }
        public MQSendClient(string serviceName, string ip, int port)
        {
            client = new MDSClient(serviceName, ip, port);
        }

        #region 发送消息
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="MQContent">消息内容</param>
        /// <param name="ServerName">接收端程序名，必须在服务端注册</param>
        public void SendMQ(string MQContent)
        {
            client.Connect();
            IOutgoingMessage message = client.CreateMessage();
            message.MessageData = Encoding.UTF8.GetBytes(MQContent);
            message.Send();
            client.Disconnect();

        }

        public void SendMQ(object obj)
        {
            client.Connect();
            IOutgoingMessage message = client.CreateMessage();
            message.MessageData = GeneralHelper.SerializeObject(obj);
            message.Send();
            client.Disconnect();
        }
       

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="MQContentList">消息内容数组</param>
        /// <param name="ServerName">接收端程序名，必须在服务端注册</param>
        public void SendMQ(List<string> MQContentList)
        {
           client.Connect();
           IOutgoingMessage message = client.CreateMessage();

            foreach (string mqc in MQContentList)
            {
                message.MessageData = Encoding.UTF8.GetBytes(mqc);

                message.Send();
            }
            client.Disconnect();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="MQContentList">消息内容数组</param>
        /// <param name="ServerNameList">接收端程序名数组，必须在服务端注册</param>
        public void SendMQ(List<string> MQContentList, List<string> ServerNameList)
        {
            if (MQContentList.Count == ServerNameList.Count)
            {

               client.Connect();

                IOutgoingMessage message = client.CreateMessage();

                for (int i = 0; i < MQContentList.Count; i++)
                {
                    message.DestinationApplicationName = ServerNameList[i];

                    message.MessageData = Encoding.UTF8.GetBytes(MQContentList[i]);

                    message.Send();
                }
                client.Disconnect();
            }
        }
        #endregion

    }

}
