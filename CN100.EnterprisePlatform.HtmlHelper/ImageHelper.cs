using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CN100.EnterprisePlatform.Configuration;

namespace CN100.EnterprisePlatform.HtmlHelper
{
    public class ImageHelper
    {
        /// <summary>
        /// 图片存储正则
        /// </summary>
        const string PRODUCT_IMG = @"^(\d{3})(\d{11})(([a-zA-Z0-9]{32})(\.jpg|\.png|\.gif|\.bmp))(=(\d{1,4})x(\d{1,4})(\.jpg|\.png|\.gif|\.bmp))?";
        /// <summary>
        /// 获取图片完整路径
        /// Web.config必须配置ImageDomain的节点
        /// </summary>
        /// <example>
        /// 配置节点
        /// <add key="ImageListDomain" value="http://img0.cn100.com,http://img1.cn100.com,http://img2.cn100.com"/>
        /// <para>
        /// imgPath="00120120702001EE920F8FB76040C8AA1A72133668DF20.jpg";
        /// </para>
        /// <code>
        /// C#
        /// string path=ImageHelper.ImagePathHelper(imgPath);
        /// HTML
        /// <img src="<%ImageHelper.ImagePathHelper(imgPath)%>" alt=""/>
        /// </code>
        /// </example>
        /// <param name="imgPath">The img path.</param>
        /// <returns>完整的图片路径</returns>
        public static string ImagePathHelper(string imgPath)
        {
            if (string.IsNullOrEmpty(imgPath))
                return imgPath;
            if (!Regex.IsMatch(imgPath, PRODUCT_IMG))
                return imgPath;
            string result = string.Empty;
            result = string.Format("{0}/{1}", Utils.ImageDomainHelper.ImageDomain, imgPath);
            return result;
        }

    }
}
