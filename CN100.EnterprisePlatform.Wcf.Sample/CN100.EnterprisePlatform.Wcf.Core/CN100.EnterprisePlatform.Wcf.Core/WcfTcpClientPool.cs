using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace CN100.EnterprisePlatform.Wcf.Core
{

    interface IWcfTcpClientPool
    {
        object GetClient();
    }
    /// <summary>
    /// WcfTcp客户端连接池类
    /// </summary>
    /// <typeparam name="T">对应的服务类型</typeparam>
    class WcfTcpClientPool<T> : IWcfTcpClientPool
    {

        private List<WcfTcpClientNode<T>> mNodes = new List<WcfTcpClientNode<T>>(4);

        /// <summary>
        /// 初始化池
        /// </summary>
        /// <param name="binding">Binding方式</param>
        /// <param name="address">服务地址</param>
        public WcfTcpClientPool(Binding binding,int connections,string[] address)
        {
            foreach (string item in address)
            {
                WcfTcpClientNode<T> node = new WcfTcpClientNode<T>(binding, item);
                node.Initialize(connections);
                mNodes.Add(node);
            }
        }

        [ThreadStatic]
        static NodeIndex mIndex;
        protected NodeIndex Index
        {
            get
            {
                if (mIndex == null)
                    mIndex = new NodeIndex();
                return mIndex;
            }
        }

        public class NodeIndex
        {
            public int Value;
        }
        /// <summary>
        /// 从池中获取一个连接
        /// </summary>
        /// <returns>object</returns>
        public object GetClient()
        {
            int count = 0;
            while (count <= mNodes.Count)
            {
                Index.Value++;
                if (Index.Value >= mNodes.Count)
                    Index.Value = 0;
                WcfTcpClientNode<T> node = mNodes[Index.Value];
                if (node.Status == NodeStatus.None)
                    return (WcfTcpClient<T>)node.Pop();
                else
                    node.Verify();
                count++;
            }
            return null;
           
        }

       
    }
}
