using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CN100.EnterprisePlatform.Wcf.Core
{
    public class WcfTcpClientNode<T>:IClientPool
    {

        private System.Collections.Queue mQueue = new System.Collections.Queue(100);

        private bool mIsDisposed = false;

        private ChannelFactory<T> mFactory;

        private string mAddress;    

        private int mLastErrorTime = Environment.TickCount;

        public WcfTcpClientNode(Binding binding,string address)
        {
            Status = NodeStatus.None;
            mFactory = new ChannelFactory<T>(binding);
            mAddress = address;
        }
  
        public NodeStatus Status
        {
            get;
            set;
        }

        public void Initialize(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (Status == NodeStatus.None)
                {
                    WcfTcpClient<T> item = createClient();
                    if (item != null)
                        Push(item);
                }
            }
        }

        private WcfTcpClient<T> createClient()
        {
            try
            {
                WcfTcpClient<T> result = new WcfTcpClient<T>();
                result.Channel = mFactory.CreateChannel(new EndpointAddress(mAddress));
                result.Pool = this;
                result.CObject = (ICommunicationObject)result.Channel;
                result.CObject.Faulted += result.OnFaulted;
                result.CObject.Open();
                Status = NodeStatus.None;
                return result;
            }
            catch (Exception e_)
            {
                
                Status = NodeStatus.Error;
                mLastErrorTime = Environment.TickCount;
                WcfClientFactory.Log.Error(e_);
                return null;
            }
        }

        private void OnVerify()
        {
            try
            {
                WcfTcpClient<T> item = createClient();
                if (item != null)
                    Push(item);

            }
            catch(Exception e_)
            {
                WcfClientFactory.Log.Error(e_);
            }
        }

        public void Verify()
        {
            if (Status == NodeStatus.Error)
            {
                if (Environment.TickCount - mLastErrorTime >= 30000)
                {
                    mLastErrorTime = Environment.TickCount;
                    Smark.Core.Functions.Action(OnVerify);
                }
            }
        }
        public object Pop()
        {
            lock (mQueue)
            {
                WcfTcpClient<T> result;
                if (mQueue.Count > 0)
                    result = (WcfTcpClient<T>)mQueue.Dequeue();
                else
                    result = createClient();
                result.Reset();
                return result;
            }
        }

        public void Push(object client)
        {
            lock (mQueue)
            {
                mQueue.Enqueue(client);
            }
        }

        public void Dispose()
        {
            lock (this)
            {
                if (mIsDisposed)
                {
                    lock (mQueue)
                    {
                        while (mQueue.Count > 0)
                        {
                            WcfTcpClient<T> item = (WcfTcpClient<T>)mQueue.Dequeue();
                            item.Pool = null;
                        }
                    }
                }
            }
        }

        public bool IsDisplsed
        {
            get { return mIsDisposed; }
        }
    }

    public enum NodeStatus
    {
        None,
        Error
    }
}
