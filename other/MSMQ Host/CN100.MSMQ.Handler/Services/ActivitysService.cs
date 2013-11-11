using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.Wcf.Core;
using CN100.ProductDetail.Const;
using Newtonsoft.Json;
using CN100.ControlCenter.IServices;
using CN100.EnterprisePlatform.Wcf.Core.Config;
using CN100.ProductDetail.BLL.Model;
using CN100.EnterprisePlatform.Utility;

namespace CN100.MSMQ.Handler.Services
{
    class ActivitysService : BaseService
    {


        public ActivitysService()
        {

        }

        public void SaveComboActivity(Int64 storeId)
        {
            try
            {
                using (WcfTcpClient<IActivityService> client = factory.CreateClient<IActivityService>())
                {
                    var item = client.Channel.GetStorePackageActivitys(storeId);
                    var key = RedisKeyConst.GetStorePackageInfoKey(storeId).Key;
                    if (item != null && item.Count > 0)
                    {

                        CN100.Redis.Client.RedisClientUtility.SetData<IList<StorePackageModel>>(key, item);
                    }
                    else
                    {
                        DeleteByKey(key);
                    }
                }


            }
            catch (Exception ex)
            {


            }




        }

        /// <summary>满就送
        /// </summary>
        /// <param name="storeId"></param>
        public void SaveFullActivity(Int64 storeId)
        {
            using (WcfTcpClient<IActivityService> client = factory.CreateClient<IActivityService>())
            {
                var item = client.Channel.GetStoreFullSends(storeId);
                string key = RedisKeyConst.GetFullActivityNoticeKey(storeId).Key;
                if (item != null && item.Count > 0)
                {

                    List<string> noFreeArea = new List<string>();
                    var splitChar = new char[] { ',', '，' };
                    var allArea = CN100.Redis.Client.RedisClientUtility.GetData<IList<FreightProvinceModel>>(RedisKeyConst.GetProvincesKey().Key);

                    foreach (var sub in item)
                    {
                        if (!string.IsNullOrWhiteSpace(sub.AreaNotFreeShipping))
                        {
                            var arr = sub.AreaNotFreeShipping.Split(splitChar);
                            foreach (var sub2 in arr)
                            {
                                var address = allArea.FirstOrDefault(it => it.Code.Equals(sub2.Trim()));
                                if (address != null)
                                {
                                    noFreeArea.Add(address.Name);
                                }
                            }
                            sub.AreaNotFreeShipping = string.Join(",", noFreeArea);
                            noFreeArea.Clear();
                        }

                    }

                    CN100.Redis.Client.RedisClientUtility.SetData<IList<FullSendModel>>(key, item);
                }
                else
                {
                    DeleteByKey(key);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void ChangeProductTuan(long ID)
        {
            var redisKey = RedisKeyConst.GetProductTuanInfoKey(ID).Key;
            using (WcfTcpClient<IActivityService> clint = factory.CreateClient<IActivityService>())
            {
                var item = clint.Channel.GetTuanActivity(ID);
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<TuanGouModel>(redisKey, item);
                }
                else
                {
                    DeleteByKey(redisKey);
                }
            }
        }
        /// <summary>活动信息变更
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="pid"></param>
        public void ChangeProductActivity(long pid)
        {
            ProductService pservice = new ProductService();
            string redisKey = RedisKeyConst.GetProductInfoKey(pid).Key;
            var prodInfo = CN100.Redis.Client.RedisClientUtility.GetData<ProductInfoModel>(redisKey);
            if (prodInfo == null)
            {

                pservice.PublishProduct(pid);
            }
            else
            {
                using (WcfTcpClient<IActivityService> clint = factory.CreateClient<IActivityService>())
                {
                    prodInfo.ProductActivityList = clint.Channel.GetProductActivitys(pid);

                }
                CN100.Redis.Client.RedisClientUtility.SetData<ProductInfoModel>(redisKey, prodInfo);
                pservice.ChangeProductSKUDetail(prodInfo.SkuList, prodInfo.ProductActivityList, RedisKeyConst.GetProductSKUKey(pid).Key);
            }


        }
    }

}
