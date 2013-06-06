// -----------------------------------------------------------------------
// <copyright file="ImageDomainHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;
    /// <summary>
    /// 图片域名帮助
    /// </summary>
    [Guid("36B570AB-B3C0-41D5-85D0-A5F0DE19FB4D")]
    public  class ImageDomainHelper
    {
        [ThreadStatic]
        private static long index;
        /// <summary>
        /// 图片域名
        /// </summary>
        public string ImageDomain
        {
            get
            {
                index++;
                return ImageSection.Instance.Urls.GetItemAt((int)index % ImageSection.Instance.Urls.Count).Url;
            }
        }        
    }
}
