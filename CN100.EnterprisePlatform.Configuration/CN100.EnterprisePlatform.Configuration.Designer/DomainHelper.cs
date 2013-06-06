// -----------------------------------------------------------------------
// <copyright file="DomainHelper.cs" company="CN100.COM">
// 域名管理
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.Configuration
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 域名管理
    /// </summary>
    [Guid("AC0FC61D-5909-4972-8501-0B36CC285421")]
    public  class DomainHelper
    {
        /// <summary>
        /// 主域名链接
        /// http://www.cn100.com
        /// </summary>
        public  string MainDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.MAINDOMAIN).Url;
            }
        }
        /// <summary>
        /// 商品域名链接
        /// </summary>
        public  string ProductDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.PRODUCT).Url;
            }
        }
        /// <summary>
        /// 订单域名链接
        /// </summary>
        public  string OrderDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.ORDER).Url;
            }
        }
        /// <summary>
        /// 支付域名链接
        /// </summary>
        public  string PayDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.PAY).Url;
            }
        }
        /// <summary>
        /// 搜索域名链接
        /// </summary>
        public  string SearchDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.SEARCH).Url;
            }
        }
        /// <summary>
        /// 注册域名链接
        /// </summary>
        public  string RegDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.REG).Url;
            }
        }
        /// <summary>
        /// 频道域名链接
        /// </summary>
        public  string ChannelDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.CHANNEL).Url;
            }
        }
        /// <summary>
        /// 团购域名链接
        /// </summary>
        public  string TuanDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.TUAN).Url;
            }
        }
        /// <summary>
        /// 店铺装饰域名链接
        /// </summary>
        public  string XiuDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.XIU).Url;
            }
        }
        /// <summary>
        /// 直通车域名链接
        /// </summary>
        public  string PromotionDomain
        {


            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.PROMOTION).Url;
            }
        }
        /// <summary>
        /// 招商中心域名链接
        /// </summary>
        public  string ZhaoShangDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.ZHAOSHANG).Url;
            }
        }
        /// <summary>
        /// 帮助中心域名链接
        /// </summary>
        public  string HelpDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.HELP).Url;
            }
        }
        /// <summary>
        /// 购物车域名链接
        /// </summary>
        public  string ShoppingCartDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.SHOPPINGCART).Url;
            }
        }
        /// <summary>
        /// 热卖频道域名链接
        /// </summary>
        public  string HotSaleDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.HOTSALE).Url;

            }
        }
        /// <summary>
        /// 商品详情页
        /// </summary>
        public string ItemDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.ITEM).Url;
            }
        }
        /// <summary>
        /// 店铺链接
        /// </summary>
        public string ShopDomain
        {
            get
            {
                return DomainSection.Instance.Urls.GetItemByKey(ConstManager.DomainConstManager.SHOP).Url;
            }
        }
    }
}
