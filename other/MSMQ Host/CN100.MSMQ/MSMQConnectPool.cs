using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Reflection;
using CN100.ProductDetail.Common.Log;

namespace CN100.MSMQ
{
    /// <summary>
    /// MSMQ客户端连接池管理
    /// </summary>
    public class PoolClientManager
    {
        #region 私有属性

        /// <summary>
        /// 连接池
        /// </summary>
        private Stack<CN100MessageQueue> queuePool = new Stack<CN100MessageQueue>();
        private CN100MessageQueue mq = null;

        /// <summary>
        /// 消息格式化器
        /// </summary>
        private IMessageFormatter MessageFormatter = new BinaryMessageFormatter();

        #endregion

        #region 构造函数

        public PoolClientManager() { }

        public PoolClientManager(string connectName)
        {
            MSMQSection section = CN100.MSMQ.MSMQSection.Instance;
            ItemConfig conf = section.Connections[connectName];
            Start(conf);
        }

        public PoolClientManager(string host, string queueName, bool isTransactional)
        {
            ItemConfig conf = new ItemConfig() { 
                host = host,
                queueName = queueName,
                isTransactional = isTransactional
            };
            Start(conf);
        }

        private string m_Host = string.Empty;
        /// <summary>
        /// 获取MSMQ服务器地址
        /// </summary>
        public string Host { get { return m_Host; } }

        private string m_QueueName = string.Empty;
        /// <summary>
        /// 获取MSMQ队列名称
        /// </summary>
        public string QueueName { get { return m_QueueName; } }

        private bool m_IsTransactional = false;
        /// <summary>
        /// 是否是事务性队列
        /// </summary>
        public bool IsTransactional { get { return m_IsTransactional; } }


        #endregion


        #region 连接池操作

        /// <summary>
        /// 获取一个链接
        /// </summary>
        /// <returns></returns>
        private CN100MessageQueue GetConnect()
        {
            lock (queuePool)
            {
                CN100MessageQueue result = null;
                if (queuePool.Count > 0)
                    result = queuePool.Pop();
                else
                    result = mq;

                if (result == null) throw new Exception("请先执行Start方法！");

                return result;
            }
        }

        /// <summary>
        /// 回收一个连接
        /// </summary>
        private void ReturnObjectToPool(CN100MessageQueue queue)
        {
            if (queue != null)
            {
                lock (queuePool)
                {
                    queuePool.Push(queue);
                }
            }
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        private void Start(ItemConfig conf)
        {
            try
            {
                mq = new CN100MessageQueue(string.Format(@"FormatName:DIRECT=TCP:{0}\private$\{1}", conf.host, conf.queueName));
                mq.Formatter = MessageFormatter;
                mq.QueueConfig = conf;

                m_Host = conf.host;
                m_QueueName = conf.queueName;
                m_IsTransactional = conf.isTransactional;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 消息发送

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="obj"></param>
        public void Send(object obj)
        {
            CN100MessageQueue queue = GetConnect();
            
            if (queue.QueueConfig.isTransactional)  //事务性消息
                SendByTransactional(queue, obj);
            else  //非事务性消息
                Send(queue, obj);
        }

        /// <summary>
        /// 发送非事务性消息
        /// </summary>
        /// <param name="queue"></param>
        private void Send(CN100MessageQueue queue, object obj)
        {
            lock (queue)
            {
                try
                {
                    queue.Send(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ReturnObjectToPool(queue);
                }
            }
        }

        /// <summary>
        /// 发送事务性消息
        /// </summary>
        /// <param name="queue"></param>
        private void SendByTransactional(CN100MessageQueue queue,object obj)
        {
            lock (queue)
            {
                MessageQueueTransaction tran = new MessageQueueTransaction();
                tran.Begin();
                try
                {
                    queue.Send(obj, tran);
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Abort();

                    throw ex;
                }
                finally
                {
                    ReturnObjectToPool(queue);
                }
            }
        }

        #endregion

        #region 消息读取

        public MessageConsumer CreateMessageConsumer()
        {
            CN100MessageQueue messageQueue = GetConnect();
            MessageConsumer consumer = new MessageConsumer(messageQueue);
            return consumer;
        }

        #endregion
    }
}
