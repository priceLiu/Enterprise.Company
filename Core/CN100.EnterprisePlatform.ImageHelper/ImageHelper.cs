using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace CN100.EnterprisePlatform.ImageHelper
{
    public class ImageHelper
    {
        const string IMAGEDOMAINLIST = "ImageListDomain";
        /// <summary>
        /// 图片存储正则
        /// </summary>
        const string PRODUCT_IMG = @"^(\d{3})(\d{11})(([a-zA-Z0-9]{32})(\.jpg|\.png))(=(\d{1,4})x(\d{1,4})(\.jpg|\.png))?";
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
            if (!Regex.IsMatch(imgPath, PRODUCT_IMG))
                return imgPath;
            string result = string.Empty;
            result = string.Format("{0}/{1}", ImageDomain, imgPath);
            return result;
        }

        static string ImageDomain
        {
            get
            {
                string result = string.Empty;

                string imageDomainList = System.Configuration.ConfigurationManager.AppSettings[IMAGEDOMAINLIST];
                if (!string.IsNullOrEmpty(imageDomainList))
                {
                    string[] imgDomain = imageDomainList.Split(',');
                    int second = Environment.TickCount % imgDomain.Length;        //取余
                    result = imgDomain[second];
                }

                return result;
            }
        }
    }
}
