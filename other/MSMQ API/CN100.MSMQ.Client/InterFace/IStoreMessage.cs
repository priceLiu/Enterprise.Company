using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.MSMQ.IAPI
{
    /// <summary>
    /// 店铺消息发送器
    /// </summary>
    public interface IStoreMessage : ICloneable
    {

        /// <summary>店铺审核
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void AuditStore(Int64 storeId);

        /// <summary>店铺基本信息修改
        /// </summary>
        /// <param name="storeId">套餐ID</param>
        void ModifyStore(Int64 storeId);
        /// <summary>店铺类目信息修改
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void ChangeStoreCategory(Int64 storeId);
        /// <summary>店铺装柜推荐变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void ChangeStoreRecommend(Int64 storeId);
        /// <summary>店铺客服变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void ChangeStoreCustomService(Int64 storeId);

        /// <summary>店铺商品类目变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void ChangeProductCategory(Int64 storeId);

        /// <summary>发布店铺
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void PublishStore(Int64 storeId);
        /// <summary>橱窗推荐变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeProductRecommend(Int64 storeId);

        /// <summary>店铺页面背景变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void ChangePageBackGround(Int64 storeId);
        /// <summary>店铺导航菜单变更
        /// </summary>
        /// <param name="storeId"></param>
        void ChangeStoreNavMenu(Int64 storeId);
        /// <summary>店铺收藏变更
        /// </summary>
        /// <param name="storeId"></param>
        void ChangeCollectStore(Int64 storeId);

    }
}
