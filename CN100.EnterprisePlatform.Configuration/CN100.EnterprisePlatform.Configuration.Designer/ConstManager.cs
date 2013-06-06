// -----------------------------------------------------------------------
// <copyright file="ConstManager.cs" company="Microsoft">
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
    /// TODO: Update summary.
    /// </summary>
    [Guid("F464A31E-02C1-4564-BF81-A4EC40F20772")]
    public class ConstManager
    {
        /// <summary>
        /// 域名常量管理
        /// </summary>
        public sealed class DomainConstManager
        {
            /// <summary>
            /// 主域名常量
            /// </summary>
            public const string MAINDOMAIN = "maindomain";
            /// <summary>
            /// 商品
            /// </summary>
            public const string PRODUCT = "product";
            /// <summary>
            /// 购物车
            /// </summary>
            public const string SHOPPINGCART = "shoppingcart";
            /// <summary>
            /// 支付
            /// </summary>
            public const string PAY = "pay";
            /// <summary>
            /// 订单
            /// </summary>
            public const string ORDER = "order";
            /// <summary>
            /// 搜索
            /// </summary>
            public const string SEARCH = "search";
            /// <summary>
            /// 帮助中心
            /// </summary>
            public const string HELP = "help";
            /// <summary>
            /// 直通车
            /// </summary>
            public const string PROMOTION = "promotion";
            /// <summary>
            /// 招商中心
            /// </summary>
            public const string ZHAOSHANG = "zhaoshang";
            /// <summary>
            /// 团购
            /// </summary>
            public const string TUAN = "tuan";
            /// <summary>
            /// 注册
            /// </summary>
            public const string REG = "reg";
            /// <summary>
            /// 频道
            /// </summary>
            public const string CHANNEL = "channel";
            /// <summary>
            /// 店铺装修
            /// </summary>
            public const string XIU = "xiu";
            /// <summary>
            /// 热卖频道
            /// </summary>
            public const string HOTSALE = "hotsale";

            /// <summary>
            /// 商品详情页
            /// </summary>
            public const string ITEM = "item";
            /// <summary>
            /// 店铺域名
            /// </summary>
            public const string SHOP = "shop";
        }

        /// <summary>
        /// 搜索常量管理
        /// </summary>
        public sealed class SearchConstManager
        {
            /// <summary>
            /// 索引库保存位置(虚拟目录)常量
            /// </summary>
           public static string PRODUCTDIRECTORY="productdirectory";
           /// <summary>
           /// 索引库保存位置(本地磁盘)常量
           /// </summary>
           public static string PRODUCTPATH ="productpath";
           /// <summary>
           /// 隔间更新索引时间常量
           /// </summary>
            public static string JOBTIME="jobtime";
            /// <summary>
            /// 索引库位置常量
            /// </summary>
            public static string DICTPATH = "dictpath";
        }

        public sealed class MQConstManager
        {
            /// <summary>
            /// 质量得分运算中心
            /// </summary>
            public static string MQQUALITYSERVICE = "MQQualityService";
        }
    }
}
