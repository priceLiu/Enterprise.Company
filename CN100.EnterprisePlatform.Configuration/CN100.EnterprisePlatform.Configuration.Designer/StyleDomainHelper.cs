// -----------------------------------------------------------------------
// <copyright file="StyleDomainHelper.cs" company="Microsoft">
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
    /// 样式域名帮助
    /// </summary>
   [Guid("2F5439F1-6E05-401A-9881-FEDE215207F5")]
    public  class StyleDomainHelper
    {
        /// <summary>
        /// 样式域名帮助
        /// </summary>
        public string StyleDomain
        {
            get
            {
                return StyleSection.Instance.Urls.GetItemAt(Environment.TickCount % StyleSection.Instance.Urls.Count).Url;
            }
        }
    }
}
