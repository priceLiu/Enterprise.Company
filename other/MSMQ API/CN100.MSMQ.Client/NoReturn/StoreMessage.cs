using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API.NoReturn
{
    /// <summary>
    /// 店铺消息发送器
    /// </summary>
    public sealed class StoreMessage :  IStoreMessage
    {
        /// <summary>店铺审核
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void AuditStore(Int64 storeId)
        {
            return;
        }

        /// <summary>店铺基本信息修改
        /// </summary>
        /// <param name="storeId">套餐ID</param>
        public void ModifyStore(Int64 storeId)
        {
            return;
        }
        /// <summary>店铺类目信息修改
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeStoreCategory(Int64 storeId)
        {
            return;
        }
        /// <summary>店铺装柜推荐变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeStoreRecommend(Int64 storeId)
        {
            return;
        }
        /// <summary>店铺客服变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeStoreCustomService(Int64 storeId)
        {
            return;
        }


        /// <summary>店铺商品类目变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeProductCategory(Int64 storeId)
        {
            return;
        }

        /// <summary>发布店铺
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void PublishStore(Int64 storeId)
        {
            return;
        }
        /// <summary>橱窗推荐变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductRecommend(Int64 storeId)
        {
            return;
        }

        /// <summary>店铺页面背景变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangePageBackGround(Int64 storeId)
        {
            return;
        }

        /// <summary>店铺导航菜单变更
        /// </summary>
        /// <param name="storeId"></param>
        public void ChangeStoreNavMenu(Int64 storeId)
        {
            return;
        }

        /// <summary>店铺收藏变更
        /// </summary>
        /// <param name="storeId"></param>
        public void ChangeCollectStore(Int64 storeId)
        {
            return;
        }



        public object Clone()
        {
            return new StoreMessage();
        }
    }
}
