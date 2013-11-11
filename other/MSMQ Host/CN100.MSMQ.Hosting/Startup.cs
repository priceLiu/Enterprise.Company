using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Messaging;
using CN100.ProductDetail.Common;
using System.Security.Policy;
using LH.JsonHelper;
using CN100.MSMQ;
using CN100.ProductDetail.Common.Enums;
using CN100.ProductDetail.Common.Log;

namespace CN100.MSMQ.Hosting
{
    public class Startup
    {
        private static List<IPlugin> PluginList = new List<IPlugin>();

        /// <summary>
        /// MQ服务端入口
        /// </summary>
        public static void Run()
        {
            try
            {
                PluginList = CN100.ProductDetail.Common.Plugins.Load<IPlugin>(AppDomain.CurrentDomain.BaseDirectory);  //加载消息处理功能模块
            }
            catch (Exception ex)
            {
                Logger.MQLog.Fatal("插件加载失败！", ex);  //致命错误
            }
            ListenMessage();  //消息侦听
        }

        /// <summary>
        /// MQ服务器链接
        /// </summary>
        private static void ListenMessage()
        {
            try
            {
                CN100.MSMQ.PoolClientManager client = new CN100.MSMQ.PoolClientManager("defaultConnect");
                MessageConsumer consumer = client.CreateMessageConsumer();
                consumer.Listener += new MessageConsumer.MessageListener(consumer_Listener);
            }
            catch (Exception ex)
            {
                Logger.MQLog.Fatal("侦听MQ消息出错，请检查MQ连接配置是否正确！", ex);
            }
        }



        /// <summary>
        /// 消息侦听
        /// </summary>
        /// <param name="message"></param>
        private static void consumer_Listener(MQMessage message)
        {
            try
            {
                message.Begin();
                MQSendRequestModel obj = Newtonsoft.Json.JsonConvert.DeserializeObject<MQSendRequestModel>(message.messageContent);

                var plugin = PluginList.Find(p => p.Module == obj.MODULE);
                if (plugin != null)
                    plugin.Process(EasyJson.Parse(obj.Arguments.ToString()));
                else
                    Logger.MQLog.Error("命令类型[" + obj.MODULE + "]未被识别！");

                // Logger.MQLog.Info("消息处理：" + message.messageContent);

                message.Commit();
            }
            catch (Exception ex)
            {
                string logmsg = "消息[" + message.messageContent.ToString() + "]处理失败，准备回滚事务！原因：" + ex.Message;
                Logger.MQLog.Error(logmsg, ex);
                message.RollBack();
            }
        }
    }
}
