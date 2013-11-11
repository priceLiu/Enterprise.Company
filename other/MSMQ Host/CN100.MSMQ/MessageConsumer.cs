using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using System.Transactions;
using CN100.ProductDetail.Common.Log;

namespace CN100.MSMQ
{
    /// <summary>
    /// 注意：本消息组件不支持获取远程MQ消息，只能是获取本地MQ消息
    /// </summary>
    public sealed class MessageConsumer
    {
        public delegate void MessageListener(MQMessage message);
        private event MessageListener m_Listener = null;
        private CN100MessageQueue messageQueue = null;

        private AutoResetEvent threadpoolProcess = new AutoResetEvent(false);  //用于控制接收，如果接收量大于处理量，则等待
        private static int MaxThreads = 30;  //线程数
        private int Counter = 0;

        /// <summary>
        /// 消息侦听回调事件（收到消息后执行）
        /// </summary>
        public MessageListener Listener
        {
            get { return m_Listener; }
            set
            {
                m_Listener = value;
                Start(m_Listener);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queue"></param>
        public MessageConsumer(CN100MessageQueue queue)
        {
            //获取所设置的线程数，如果没有则使用默认设置

            if (CN100.MSMQ.MSMQSection.Instance != null &&
                CN100.MSMQ.MSMQSection.Instance.Connections != null &&
                CN100.MSMQ.MSMQSection.Instance.Connections.MaxThreads > 1)
            MaxThreads = CN100.MSMQ.MSMQSection.Instance.Connections.MaxThreads;


            messageQueue = queue;


            StringBuilder logsb = new StringBuilder("侦听消息开始！");
            logsb.Append("MSMQ服务器地址：" + queue.QueueConfig.host + "；");
            logsb.Append("队列名称：" + queue.QueueConfig.queueName + "；");
            logsb.Append("是否是事务性队列：" + queue.QueueConfig.isTransactional + "；");
            logsb.Append("消息接收线程数：" + MaxThreads + "；");

            Logger.MQLog.Info(logsb.ToString());
        }


        /// <summary>
        /// 启动消息侦程序线程
        /// </summary>
        private void Start(MessageListener listener)
        {
            ThreadPool.STPStartInfo stp = new ThreadPool.STPStartInfo();
            stp.MaxStackSize = MaxThreads;
            stp.MaxWorkerThreads = MaxThreads;
            ThreadPool.SmartThreadPool smartThreadpool = new ThreadPool.SmartThreadPool(stp);

            while (true)
            {
                MQMessage message = null;
                if (messageQueue.QueueConfig.isTransactional)
                {
                    MessageQueueTransaction tran = new MessageQueueTransaction();
                    message = new MQMessage(tran);
                    message.Begin();
                    message.messageContent = messageQueue.Receive(tran).Body.ToString();
                }
                else
                {
                    message = new MQMessage(null);
                    message.messageContent = messageQueue.Receive().Body.ToString();
                }
                System.Threading.Interlocked.Increment(ref Counter);
                while (Counter >= MaxThreads)
                {
                    System.Threading.Thread.Sleep(100);
                }
                smartThreadpool.QueueWorkItem(CallBack, message);
            }
        }
        
        /// <summary>
        /// 回调侦听事件
        /// </summary>
        /// <param name="obj"></param>
        private void CallBack(object obj)
        {
            if (obj != null && Listener != null)
            {
                System.Threading.Interlocked.Decrement(ref Counter);
                Listener((MQMessage)obj);
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
