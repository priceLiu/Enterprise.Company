using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Wcf.Core
{
    /// <summary>
    /// 相关Wcf异步类
    /// </summary>
    public class CN100WcfException:Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public CN100WcfException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public CN100WcfException(string msg)
            : base(msg)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innererror"></param>
        public CN100WcfException(string msg, Exception innererror)
            : base(msg, innererror)
        {
        }

        /// <summary>
        /// 获取一个描述ServiceContract标记不存在的异常
        /// </summary>
        /// <returns>CN100WcfException</returns>
        public static CN100WcfException SERVER_CONTRACT_NOFOUND()
        {
            return new CN100WcfException("ServiceContract attribute not found!");
        }

        public static CN100WcfException CONFIG_NOTFOUND(string name)
        {
            return new CN100WcfException(string.Format( "{0} config selection not found!",name));
        }
    }
}
