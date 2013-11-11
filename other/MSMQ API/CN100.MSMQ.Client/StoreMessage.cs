using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API
{
    /// <summary>
    /// 店铺消息发送器
    /// </summary>
    public sealed class StoreMessage : Client,IStoreMessage
    { 
        /// <summary>店铺审核
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void AuditStore(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.AuditStore)));
        }

        /// <summary>店铺基本信息修改
        /// </summary>
        /// <param name="storeId">套餐ID</param>
        public void ModifyStore(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ModifyStore)));
        }
        /// <summary>店铺类目信息修改
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeStoreCategory(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeStoreCategory)));
        }
        /// <summary>店铺装柜推荐变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeStoreRecommend(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeStoreRecommend)));
        }
        /// <summary>店铺客服变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeStoreCustomService(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeStoreCustomService)));
        }


        /// <summary>店铺商品类目变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeProductCategory(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeProductCategory)));
        }

        /// <summary>发布店铺
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void PublishStore(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.PublishStore)));
        }
        /// <summary>橱窗推荐变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductRecommend(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeProductRecommend)));
        }

        /// <summary>店铺页面背景变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangePageBackGround(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangePageBG)));
        }

        /// <summary>店铺导航菜单变更
        /// </summary>
        /// <param name="storeId"></param>
        public void ChangeStoreNavMenu(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeStoreNavMenu)));
        }

        /// <summary>店铺收藏变更
        /// </summary>
        /// <param name="storeId"></param>
        public void ChangeCollectStore(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Store, string.Format(ArgumentFormart, storeId, ActionEnum.CollectStore)));
        }

        //TODO:停用店铺

        public object Clone()
        {
            return new StoreMessage();
        }
    }
}
