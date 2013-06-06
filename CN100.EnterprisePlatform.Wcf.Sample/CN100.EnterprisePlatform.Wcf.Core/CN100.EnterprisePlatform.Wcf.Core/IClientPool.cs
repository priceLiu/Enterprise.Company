using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Wcf.Core
{
    /// <summary>
    /// 连接池
    /// </summary>
    interface IClientPool:IDisposable
    {
       
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns>object</returns>
        object Pop();

        /// <summary>
        /// 当前池是否已经释放
        /// </summary>
        bool IsDisplsed
        {
            get;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="client">object</param>
        void Push(object client);
        
    }
}
