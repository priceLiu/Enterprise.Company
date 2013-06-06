using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CN100.EnterprisePlatform.Configuration
{
    /// <summary>
    /// 工具类
    /// </summary>
    [Guid("4169CA1F-56F2-41F7-B487-20562942F42C")]
    public class Utils
    {
        private static readonly StyleDomainHelper mStyleDomainHelper = new StyleDomainHelper();
        /// <summary>
        /// 样式域名帮助
        /// </summary>
        public static StyleDomainHelper StyleDomainHelper
        {
            get
            {
                return mStyleDomainHelper;
            }
        }

        private static readonly ImageDomainHelper mImageDomainHelper = new ImageDomainHelper();

        /// <summary>
        /// 图片域名帮助
        /// </summary>
        public static ImageDomainHelper ImageDomainHelper
        {
            get
            {
                return mImageDomainHelper;
            }
        }

        private static readonly DomainHelper mDomainHelper = new DomainHelper();
        /// <summary>
        /// 域名帮助
        /// </summary>
        public static DomainHelper DomainHelper
        {
            get
            {
                return mDomainHelper;
            }
        }

      

        private static readonly MQConfigHelper mMQConfigHelper = new MQConfigHelper();
        /// <summary>
        /// MQ配置节帮助
        /// </summary>
        public static MQConfigHelper MQConfigHelper
        {
            get
            {
                return mMQConfigHelper;
            }
        }
        /// <summary>
        /// Email配置节帮助
        /// </summary>
        public static EmailConfigHelper EmailConfigHelper
        {
            get
            {
                return new EmailConfigHelper();
            }
        }

    }
}
