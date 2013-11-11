using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using CN100.ProductDetail.Common.Enums;

namespace CN100.MSMQ.API
{
    public class Client
    {
        /// <summary> {"ID":{0},"Action":"{1}"}
        /// </summary>
        protected static string ArgumentFormart = "{{\"ID\":{0},\"Action\":\"{1}\"}}";

        private CN100.MSMQ.PoolClientManager msmqClient = new CN100.MSMQ.PoolClientManager("defaultConnect");

        /// <summary>
        /// 向MQ发送请求
        /// </summary>
        /// <param name="requestParam">请求参数</param>
        protected void SendRequest(string requestParam)
        {
            msmqClient.Send(requestParam);
        }
    }
}
