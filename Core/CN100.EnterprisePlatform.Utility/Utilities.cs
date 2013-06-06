// -----------------------------------------------------------------------
// <copyright file="Utils.cs" company="Microsoft">
// CN100.EnterprisePlatform.Utility统一调用接口
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// CN100.EnterprisePlatform.Utility统一调用接口
    /// </summary>
    public static class Utilities
    {
        private static MailUtils _MailUtils = new MailUtils();
        /// <summary>
        /// 邮件发送
        /// </summary>
        public static MailUtils MailSend
        {
            get
            {
                return _MailUtils;
            }
        }
    }
}
