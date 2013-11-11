using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common;
using CN100.ProductDetail.Common.Enums;
using LH.JsonHelper;
using CN100.MSMQ.Handler.Services;

namespace CN100.MSMQ.Handler
{
    public class StoreHanlder : IPlugin
    {
        public ModuleEnum Module
        {
            get { return ModuleEnum.Store; }
        }
        StoreService storeService;
        ActivitysService activityService;

        FreightService freightService;
        /// <summary>店铺ID
        /// </summary>
        public Int64 StoreId { get; set; }


        public StoreHanlder()
        {
            storeService = new StoreService();
            activityService = new ActivitysService();
            freightService = new FreightService();
        }

        public void Process(JsonObject obj)
        {
            if (obj.IsNull || obj.IsEmpty)
            {
                return;
            }
            StoreId = obj["ID"].ToLong(0);
            if (StoreId <= 0)
            {
                return;
            }
            if (obj["Action"].IsNull || obj["Action"].IsEmpty)
            {
                return;
            }

            //try
            //{
            Action = (ActionEnum)Enum.Parse(typeof(ActionEnum), obj["Action"].Value);
            switch (Action)
            {

                case ActionEnum.AuditStore:
                    AuditStore();
                    break;
                case ActionEnum.ChangeComboActivity:
                    activityService.SaveComboActivity(StoreId);
                    break;
                case ActionEnum.ChangeFullSend:
                    activityService.SaveFullActivity(StoreId);
                    break;
                case ActionEnum.ChangePageBG:
                    ChangePageBG();
                    break;
                case ActionEnum.ChangeStoreCategory:
                    storeService.SaveStoreCategory(StoreId);
                    break;
                case ActionEnum.ChangeStoreCustomService:
                    storeService.SaveCustomServices(StoreId);
                    break;

                case ActionEnum.ChangeProductCategory:  //改变商品类目，缺少IBLL
                    storeService.SaveProductCategory(StoreId);
                    break;
                case ActionEnum.ChangeStoreRecommend:  //保存店铺评论，缺少IBLL
                    storeService.SaveChangeStoreRecommend(StoreId);
                    break;

                case ActionEnum.ModifyStore:
                    PublishStore();
                    break;

                case ActionEnum.PublishStore:
                    PublishStore();
                    break;

                //case ActionEnum.ChangeStoreNavMenu:
                //    storeService.ChangeStoreNavMenu(StoreId);
                //    break;
                case ActionEnum.CollectStore:
                    //storeService
                    break;
                default: break;
            }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }

        private void ChangePageBG()
        {
            storeService.SaveStorePageBackGround(StoreId);
        }
        /// <summary>店铺审核
        /// </summary>
        private void AuditStore()
        {
            storeService.SaveStoreInfo(StoreId);
            storeService.SaveCustomServices(StoreId);
            storeService.SaveStoreEvaluat(StoreId);
            PublishStore();
            //storeService.ChangeStoreNavMenu(StoreId);
        }
        /// <summary>发布店铺装修
        /// </summary>
        /// <param name="StoreId"></param>
        private void PublishStore()
        {
            
            storeService.SaveStoreCategory(StoreId);
            storeService.SavePageModules(StoreId);
            storeService.SaveStoreTopModule(StoreId);
            storeService.SaveStoreButtom(StoreId);
            storeService.SaveStorePageBackGround(StoreId);
           
        }

        public ActionEnum Action
        {
            get;
            set;
        }
    }
}
