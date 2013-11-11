using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Transactions;

namespace CN100.MSMQ
{
    public class MQMessage
    {
        /// <summary>
        /// 事务
        /// </summary>
        private MessageQueueTransaction tran = null;

        public MQMessage(MessageQueueTransaction tran)
        {
            this.tran = tran;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string messageContent { get; set; }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            if (tran != null)
                tran.Commit();
        }

        /// <summary>
        /// 事务开始
        /// </summary>
        public void Begin()
        {
            if (tran != null && tran.Status == MessageQueueTransactionStatus.Initialized)
                tran.Begin();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollBack()
        {
            if (tran != null)
                tran.Abort();
        }
    }
}
