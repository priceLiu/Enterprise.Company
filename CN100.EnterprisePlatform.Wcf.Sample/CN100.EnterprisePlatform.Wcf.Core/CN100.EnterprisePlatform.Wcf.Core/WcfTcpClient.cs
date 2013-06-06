using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
namespace CN100.EnterprisePlatform.Wcf.Core
{
    /// <summary>
    /// WcfTcp连接对象
    /// </summary>
    /// <typeparam name="T">服务通道类型</typeparam>
    public class WcfTcpClient<T>:IDisposable
    {
        internal WcfTcpClient()
        {
            IsInvokeError = false;
            ActiveTime = DateTime.Now;
        }

        internal IClientPool Pool
        {
            get;
            set;
        }

        internal ICommunicationObject CObject
        {
            get;
            set;
        }

        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime ActiveTime
        {
            get;
            set;
        }

        /// <summary>
        /// 获取对应的服务通道
        /// </summary>
        public T Channel
        {
            get;
            internal set;
        }

        internal void Reset()
        {
            lock (this)
            {
                mIsDisposed = false;
            }
        }

        private bool mIsDisposed = false;

        /// <summary>
        /// 释放当前对象，使用完成注意调用此方法s
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                if (!mIsDisposed)
                {
                    mIsDisposed = true;
                   
                    if (!IsInvokeError)
                    {
                        if (Pool != null && !Pool.IsDisplsed)
                        {
                            Pool.Push(this);

                        }
                        else
                        {
                            Pool = null;
                        }
                    }
                    else
                    {
                        Pool = null;
                        try
                        {
                            CObject.Close();
                        }
                        catch
                        {
                        }
                    }

                }
            }
        }

        internal bool IsInvokeError
        {
            get;
            set;
        }

        internal void OnClose(object sender, EventArgs e)
        {
            
        }

        internal void OnFaulted(object sender, EventArgs e)
        {
            IsInvokeError = true;
           
        }

    }
}
