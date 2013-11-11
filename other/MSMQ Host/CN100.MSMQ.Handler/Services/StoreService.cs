using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LH.JsonHelper;
using CN100.ControlCenter.IServices;
using Newtonsoft.Json;
using CN100.ProductDetail.Const;
using CN100.ProductDetail.Common.Enums;
using CN100.ProductDetail.BLL.Model;

using CN100.EnterprisePlatform.Wcf.Core;
using CN100.EnterprisePlatform.Wcf.Core.Config;

namespace CN100.MSMQ.Handler.Services
{


    public class StoreService : BaseService
    {

        const string moduleTagStr = "ModuleTag";
        const string moduleIdStr = "ModuleID";


        /// <summary>保存店铺轮播
        /// </summary>
        /// <param name="ModuleJson"></param>
        /// <param name="redisKey"></param>
        public void SaveSlider(JsonObject ModuleJson, string redisKey)
        {
            if (!CheckJson(ModuleJson))
            {
                DeleteByKey(redisKey);
                return;
            }
            var imageList = ModuleJson["ImageList"];
            if (imageList.IsNull || imageList.ValueType != JsonValueType.Array)
            {
                DeleteByKey(redisKey);
                return;
            }
            StoreCarouselModel item = new StoreCarouselModel();
            item.IsSHowTitle = ModuleJson["IsShowTitle"].ToBool(false);
            item.ModuleTitel = ModuleJson["ModuleTitle"].Value;
            item.ModuleId = ModuleJson["ModuleID"].Value;
            item.ModuleTag = ModuleJson["ModuleTag"].Value;
            item.SliderEffect = (SliderEffectEnum)ModuleJson["SliderEffect"].ToInt(1);
            item.Direction = ModuleJson["Direction"].Value;
            item.IsSHowTitle = ModuleJson["IsShowTitle"].ToBool(false);
            item.ModelHeight = ModuleJson["Height"].Value;
            item.ModelWidth = ModuleJson["Width"].Value;
            item.ImageList = new List<ImageLinkModel>();
            foreach (var sub in imageList)
            {
                item.ImageList.Add(new ImageLinkModel()
                {
                    ImageSrc = sub["ImageSrc"].Value
                    ,
                    LinkUrl = sub["Href"].Value
                    ,
                    LinkWorld = sub["LinkWord"].Value
                    ,
                    Target = sub["Target"].Value
                    ,
                    Title = sub["Description"].Value
                    ,
                    ImageHeight = sub["ImageHeight"].Value
                    ,
                    ImageWidth = sub["Imagewidth"].Value,
                });
            }

            CN100.Redis.Client.RedisClientUtility.SetData<StoreCarouselModel>(redisKey, item);

        }

        /// <summary>保存店铺排行，推荐及推广
        /// </summary>
        /// <param name="ModuleJson"></param>
        /// <param name="redisKey"></param>
        /// <param name="storeId"></param>
        public void SaveProductList(JsonObject ModuleJson, string redisKey, Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {
                var item = clint.Channel.GetStoreProductList(storeId, EasyJson.ToJsonString(ModuleJson));
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<ProductListModel>(redisKey, item);
                }
                else
                {
                    DeleteByKey(redisKey);
                }

            }
        }

        /// <summary>保存店铺自定义模块
        /// </summary>
        /// <param name="ModuleJson"></param>
        /// <param name="redisKey"></param>
        public void SaveCustomModule(string moduleJsonStr, string redisKey)
        {
            StoreCustomModel item = JsonConvert.DeserializeObject<StoreCustomModel>(moduleJsonStr);
            if (item != null)
            {
                CN100.Redis.Client.RedisClientUtility.SetData<StoreCustomModel>(redisKey, item);
            }
            else
            {
                DeleteByKey(redisKey);
            }

        }

        /// <summary>保存店铺搜索
        /// </summary>
        /// <param name="ModuleJson"></param>
        /// <param name="key"></param>
        public void SaveStoreSearch(JsonObject ModuleJson, string key)
        {
            if (!CheckJson(ModuleJson))
            {
                DeleteByKey(key);
                return;
            }
            var item = new StoreSearchModel();
            item.ModuleId = ModuleJson["ModuleID"].Value;
            item.IsSHowTitle = ModuleJson["IsShowTitle"].ToBool(false);
            item.ModuleTag = ModuleJson["ModuleTag"].Value;
            item.ModuleTitel = ModuleJson["ModuleTitle"].Value;
            item.IsShowPriceFilter = ModuleJson["IsShowPriceFilter"].ToBool(false);
            item.HotWord = EasyJson.ToJsonString(ModuleJson["LinkList"]);

            CN100.Redis.Client.RedisClientUtility.SetData<StoreSearchModel>(key, item);

        }

        /// <summary>保存店铺
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveStoreInfo(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {
                string key = RedisKeyConst.GetStoreInfoKey(storeId).Key;
                var item = clint.Channel.GetStoreInfo(storeId);
                CN100.Redis.Client.RedisClientUtility.SetData<StoreBaseInfoModel>(key, item);
            }
        }

        /// <summary>保存店铺页面背景设置
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveStorePageBackGround(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var item = clint.Channel.GetStoreBackGround(storeId, VersionEnum.Published);
                string key = RedisKeyConst.GetStorePageBGKey(storeId).Key;
                CN100.Redis.Client.RedisClientUtility.SetData<StoreBackGroundModel>(key, item);
            }
        }

        /// <summary>保存店铺页尾
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveStoreButtom(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var item = clint.Channel.GetStorePageFooter(storeId);
                string key = RedisKeyConst.GetStorePageFooterKey(storeId).Key;
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<StoreBottomModel>(key, item);
                }
                else
                {
                    DeleteByKey(key);
                }

            }
        }

        /// <summary>保存店铺页头
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveStoreTopModule(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var item = clint.Channel.GetStorePageHeader(storeId);
                string key = RedisKeyConst.GetStorePageHeaderKey(storeId).Key;
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<StoreTopModel>(key, item);
                }
                else
                {
                    DeleteByKey(key);
                }
            }
        }

        /// <summary>保存店铺类目信息
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveStoreCategory(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var item = clint.Channel.GetStoreCategory(storeId);
                string key = RedisKeyConst.GetStoreCategoryKey(storeId).Key;
                if (item != null && item.Count > 0)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<IList<StoreCategoryModel>>(key, item);
                }
                else
                {
                    DeleteByKey(key);
                }
            }

            SaveStoreTopModule(storeId);  //修改页头
        }

        /// <summary>保存店铺客服信息
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveCustomServices(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var item = clint.Channel.GetCustomServices(storeId);
                string key = RedisKeyConst.GetStoreCustomServiceKey(storeId).Key;
                if (item != null && item.Count > 0)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<IList<StoreCustomServiceModel>>(key, item);
                }
                else
                {
                    DeleteByKey(key);
                }

            }
        }

        /// <summary>保存店铺评价
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveStoreEvaluat(Int64 storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var item = clint.Channel.GetStoreEvaluation(storeId);
                var key = RedisKeyConst.GetStoreEvaluatKey(storeId).Key;
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<StoreEvaluationModel>(key, item);
                }
                else
                {
                    DeleteByKey(key);
                }
            }
        }

        /// <summary>保存店铺装修模块
        /// </summary>
        /// <param name="storeId"></param>
        public void SavePageModules(Int64 storeId)
        {

            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var storePageContent = clint.Channel.GetStorePageContents(storeId, VersionEnum.Published, PageTypeEnum.DetailPage);
                string moduleTag = string.Empty;
                string redisKey = string.Empty;
                JSONPage jsonPage;
                if (storePageContent != null && storePageContent.Count > 0)
                {

                    //删除所有PageContent
                    CN100.Redis.Client.RedisClientUtility.DelBySearchKey(RedisKeyConst.GetStorePageContentKey(storeId, 0).Key);

                    foreach (var item in storePageContent)
                    {
                        if (item != null && item.PageContent != null)
                        {


                            jsonPage = JsonConvert.DeserializeObject<JSONPage>(item.PageContent);

                            var moduleList = GetStoreModules(jsonPage.PageLayoutUnits);
                            CN100.Redis.Client.RedisClientUtility.SetData<PlatformDisclaimerModel>(RedisKeyConst.GetStorePageContentKey(item.StoreId, item.PageId).Key, item);
                            #region 处理店铺所有受装修配置影响的模块
                            //插入前先删除掉页面的所有Module数据
                            CN100.Redis.Client.RedisClientUtility.DelBySearchKey( RedisKeyConst.GetModuleKey( item.StoreId, item.PageId, "*",string.Empty).Key);
                            foreach (var moduleItem in moduleList)
                            {
                                var jobj = (Newtonsoft.Json.Linq.JObject)moduleItem;
                                moduleTag = jobj[moduleTagStr].ToString();
                                JsonObject ModuleJson = EasyJson.Parse(RegexFixSystemChar.Replace(moduleItem.ToString(), string.Empty));

                                redisKey = RedisKeyConst.GetModuleKey(item.StoreId, item.PageId, ModuleJson[moduleIdStr].Value, moduleTag).Key;
                                switch (moduleTag)
                                {
                                    case ModuleTag.ProductSearch: //搜索模块　
                                    case ModuleTag.HeadSearch: //头部搜索模块
                                        SaveStoreSearch(ModuleJson, redisKey);
                                        break;
                                    case ModuleTag.Customer: //自定义模块 
                                        SaveCustomModule(moduleItem.ToString(), redisKey);
                                        break;
                                    case ModuleTag.Slider:  //轮播模块
                                        SaveSlider(ModuleJson, redisKey);
                                        break;

                                    case ModuleTag.RecommProduct:
                                    case ModuleTag.PromotionProduct:
                                    case ModuleTag.RankingProduct: //排行模块
                                        SaveProductList(ModuleJson, redisKey, storeId);
                                        break;
                                    default: break;
                                }



                            }
                            #endregion
                        }

                    }

                }
            }


        }
        
        /// <summary>
        /// 修改店铺商品类目
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveProductCategory(long storeId)
        {
            SaveChangeStoreRecommend(storeId);
        }

        /// <summary>
        /// 变更商品推荐
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveChangeStoreRecommend(long storeId)
        {
            using (WcfTcpClient<IStoreService> clint = factory.CreateClient<IStoreService>())
            {

                var storePageContent = clint.Channel.GetStorePageContents(storeId, VersionEnum.Published, PageTypeEnum.DetailPage);
                string moduleTag = string.Empty;
                string redisKey = string.Empty;
                JSONPage jsonPage;
                if (storePageContent != null && storePageContent.Count > 0)
                {
                    foreach (var item in storePageContent)
                    {
                        if (item != null)
                        {
                            jsonPage = JsonConvert.DeserializeObject<JSONPage>(item.PageContent);
                            var moduleList = GetStoreModules(jsonPage.PageLayoutUnits);
                            CN100.Redis.Client.RedisClientUtility.SetData( RedisKeyConst.GetStorePageContentKey(item.StoreId, item.PageId).Key, JsonConvert.SerializeObject(item));
                            #region 处理店铺所有受装修配置影响的模块
                            //插入前先删除掉页面的所有Module数据
                            CN100.Redis.Client.RedisClientUtility.DelBySearchKey( RedisKeyConst.GetModuleKey( item.StoreId, item.PageId, "*",string.Empty).Key);
                            foreach (var moduleItem in moduleList)
                            {
                                var jobj = (Newtonsoft.Json.Linq.JObject)moduleItem;
                                moduleTag = jobj[moduleTagStr].ToString();
                                JsonObject ModuleJson = EasyJson.Parse(RegexFixSystemChar.Replace(moduleItem.ToString(), string.Empty));
                                redisKey =  RedisKeyConst.GetModuleKey( item.StoreId, item.PageId, ModuleJson[moduleIdStr].Value,moduleTag).Key;
                                switch (moduleTag)
                                {
                                    case ModuleTag.RankingProduct: //排行模块
                                    case ModuleTag.RecommProduct:
                                    case ModuleTag.PromotionProduct:
                                        SaveProductList(ModuleJson, redisKey, storeId);
                                        break;
                                    default: break;
                                }
                            }
                            #endregion
                        }

                    }

                }
            }
        }

        private IList<object> GetStoreModules(List<PageLayoutUnit> pageLayoutUnitList)
        {
            List<object> ret = new List<object>();

            foreach (var unit in pageLayoutUnitList)
            {
                foreach (var moduleItem in unit.Moduels)
                {
                    ret.Add(moduleItem);
                }
            }
            return ret;
        }

        private bool CheckJson(JsonObject ModuleJson)
        {
            if (ModuleJson == null || ModuleJson.IsNull || ModuleJson.IsEmpty)
            {
                return false;
            }
            return true;
        }

    }
}
