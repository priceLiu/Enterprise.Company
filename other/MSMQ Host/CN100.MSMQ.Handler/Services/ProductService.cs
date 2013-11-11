using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LH.JsonHelper;
using Newtonsoft.Json;
using CN100.ProductDetail.Const;
using CN100.ProductDetail.Common.Enums;
using CN100.ProductDetail.BLL.Model;
using CN100.EnterprisePlatform.Wcf.Core;
using CN100.EnterprisePlatform.Wcf.Core.Config;
using CN100.ControlCenter.IServices;

namespace CN100.MSMQ.Handler.Services
{
    public class ProductService : BaseService
    {        
        /// <summary>产品状态变更
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="pid"></param>
        public void ChangeProductStatus(long pid)
        {
            PublishProduct(pid);
        }

        /// <summary>产品删除
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="pid"></param>
        public void DeleteProduct(long pid)
        {

            CN100.Redis.Client.RedisClientUtility.DelBySearchKey("Pro_*_" + pid);
        }

        /// <summary>产品修改
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="pid"></param>
        public void ModifyProduct(long pid)
        {
            PublishProduct(pid);
        }

        /// <summary>产品发布
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="pid"></param>
        public void PublishProduct(long pid)
        {

            string ProductInfoKey = RedisKeyConst.GetProductInfoKey(pid).Key;  //商品信息

            //获取商品信息
            using (WcfTcpClient<IProductService> clint = factory.CreateClient<IProductService>())
            {
                ProductInfoModel productInfo = clint.Channel.GetProductInfo(pid);
                //保存商品数据
                if (productInfo != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData(ProductInfoKey, productInfo);
                }
                else
                {
                    CN100.Redis.Client.RedisClientUtility.DelBySearchKey("Pro_*_" + pid);
                    return;
                }
            }
            //生成SKU价格
            ChangeProductSKUDetail(pid);
            //生成商品统计信息
            ChangeProductStatistics(pid);
            //生成默认运费模板
            ChangeProductDefaultFreight(pid);
        }

        /// <summary> SKU价格列表变更
        /// </summary>
        /// <param name="pid"></param>
        public void ChangeProductSKUDetail(long pid)
        {
            IList<ProductSKUModel> productSKUList;
            IList<ProductActivityItem> activityList;
            try
            {
                string productKey = RedisKeyConst.GetProductInfoKey(pid).Key;
                var productInfo = CN100.Redis.Client.RedisClientUtility.GetData<ProductInfoModel>(productKey);
                if (productInfo == null)
                {
                    return;
                }

                //获取SKU信息
                productSKUList = productInfo.SkuList;
                activityList = productInfo.ProductActivityList;
                ChangeProductSKUDetail(productSKUList, activityList, RedisKeyConst.GetProductSKUKey(pid).Key);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ChangeProductSKUDetail(IList<ProductSKUModel> productSKUList, IList<ProductActivityItem> activityList, string productSKUKey)
        {
            List<SKUDetailModel> list = new List<SKUDetailModel>();
            if (productSKUList == null || productSKUList.Count < 1)
            {
                return;
            }
            if (activityList != null && activityList.Count > 0)
            {
                foreach (var item in activityList)
                {
                    if (item.SKU < 1)
                    {
                        foreach (var skuItem in productSKUList)
                        {
                            list.Add(new SKUDetailModel()
                            {
                                SKUBeginTime = item.StartTime ?? DateTime.MinValue,
                                SKUEndTime = item.EndTime ?? DateTime.Now.AddYears(100),
                                SKUCode = skuItem.SKUCode,
                                SKUID = skuItem.Id,
                                SkuMarketPrice = skuItem.SkuMarketPrice,
                                Level = (int)item.Event,
                                SaleUnitPrice = item.Event == ActivityTypeEnum.Discount ? (skuItem.SaleUnitPrice * item.DiscountRate) / 10 : item.Price
                            });
                        }
                    }
                    else
                    {
                        var temSku = productSKUList.FirstOrDefault(it => it.Id == item.SKU);
                        if (temSku == null)
                        {
                            continue;
                        }
                        list.Add(new SKUDetailModel()
                        {
                            SKUBeginTime = item.StartTime ?? DateTime.MinValue,
                            SKUEndTime = item.EndTime ?? DateTime.Now.AddYears(100),
                            SKUCode = temSku.SKUCode,
                            SKUID = temSku.Id,
                            SkuMarketPrice = temSku.SkuMarketPrice,
                            Level = (int)item.Event,
                            SaleUnitPrice = item.Event == ActivityTypeEnum.Discount ? (temSku.SaleUnitPrice * item.DiscountRate) / 10 : item.Price
                        });
                    }
                }
            }
            foreach (var subSku in productSKUList)
            {
                list.Add(new SKUDetailModel()
                {
                    SKUBeginTime = DateTime.Now.AddYears(-1),
                    SKUEndTime = DateTime.Now.AddYears(100),
                    SKUCode = subSku.SKUCode,
                    SKUID = subSku.Id,
                    SkuMarketPrice = subSku.SkuMarketPrice,

                    SaleUnitPrice = subSku.SaleUnitPrice,
                    Level = -9999
                });
            }
            //保存SKU信息
            if (list != null && list.Count > 0)
            {
                CN100.Redis.Client.RedisClientUtility.SetData<IList<SKUDetailModel>>(productSKUKey, list);
            }
            else
            {
                DeleteByKey(productSKUKey);
            }
        }

        public void ChangeProductQty(long pid)
        {
            var proKey = RedisKeyConst.GetProductInfoKey(pid).Key;
            var item = CN100.Redis.Client.RedisClientUtility.GetData<ProductInfoModel>(proKey);


            if (item == null)
            {
                return;
            }
            using (WcfTcpClient<IProductService> clint = factory.CreateClient<IProductService>())
            {
                item.ProductStoreQty = clint.Channel.GetProductQty(pid);

                CN100.Redis.Client.RedisClientUtility.SetData<ProductInfoModel>(proKey, item);
            }
        }

        public void ChangeProductStatistics(Int64 pid)
        {
            var proKey = RedisKeyConst.GetProductStatisticsKey(pid).Key;

            using (WcfTcpClient<IProductService> clint = factory.CreateClient<IProductService>())
            {
                var item = clint.Channel.GetProductStatistics(pid);
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<ProductStatisticsModel>(proKey, item);
                }
                else
                {
                    DeleteByKey(proKey);
                }
            }
        }

        /// <summary>保存商品默认运费
        /// </summary>
        /// <param name="pid"></param>
        public void ChangeProductDefaultFreight(Int64 pid)
        {
            var proKey = RedisKeyConst.GetProductDefaultFreightKey(pid).Key;

            using (WcfTcpClient<IFreightTemplateService> clint = factory.CreateClient<IFreightTemplateService>())
            {
                var item = clint.Channel.GetProductDetailFreight(pid);
                if (item != null && item.Count > 0)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<IList<ProductFreightModel>>(proKey, item);
                }
                else
                {
                    DeleteByKey(proKey);
                }
            }
        }

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="ids"></param>
        public void DeleteProduct(string ids)
        {
            var keys = ids.Split(',');
            Int64 id = 0;
            foreach (var item in keys)
            {
                if (Int64.TryParse(item, out id))
                {
                    DeleteProduct(id);
                }
            }

        }

        /// <summary>
        /// 商品状态批量变更
        /// </summary>
        /// <param name="Ids"></param>
        public void ChangeProductStatusList(string Ids)
        {
            if (string.IsNullOrWhiteSpace(Ids)) return;
            string[] productIdstrs = Ids.Split(',');
            if (productIdstrs == null || productIdstrs.Length <= 0) return;

            List<long> productIdList = new List<long>();

            foreach (string item in productIdstrs)
            {
                productIdList.Add(CN100.ProductDetail.Common.DataTypeConvert.ToLong(item, 0));
            }

            Dictionary<long, int> productIdDir = null;

            //根据产品ID获取产品状态
            using (WcfTcpClient<IProductService> clint = factory.CreateClient<IProductService>())
            {
                productIdDir = clint.Channel.GetProductStatusList(productIdList.ToArray());
            }

            if (productIdDir == null || productIdDir.Count <= 0) return;

            //修改Redis中的产品状态

            foreach (KeyValuePair<long, int> item in productIdDir)
            {
                //获取商品信息
                string ProductInfoKey = RedisKeyConst.GetProductInfoKey(item.Key).Key;
                ProductInfoModel productObj = CN100.Redis.Client.RedisClientUtility.GetData<ProductInfoModel>(ProductInfoKey);
                productObj.Status = item.Value;
                CN100.Redis.Client.RedisClientUtility.SetData(ProductInfoKey, productObj);
            }
        }

        /// <summary>变更SKU数据
        /// </summary>
        /// <param name="id">商品ID</param>
        public void ChangeProductSKU(long id)
        {
            string redisKey = RedisKeyConst.GetProductInfoKey(id).Key;
            ProductInfoModel prodInfo = CN100.Redis.Client.RedisClientUtility.GetData<ProductInfoModel>(redisKey);
            if (prodInfo == null)
            {
                PublishProduct(id);
            }
            else
            {
                using (WcfTcpClient<IProductService> clint = factory.CreateClient<IProductService>())
                {
                    prodInfo.SkuList = clint.Channel.GetProductSKUList(id);
                }
                if (prodInfo != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<ProductInfoModel>(redisKey, prodInfo);
                    ChangeProductSKUDetail(prodInfo.SkuList, prodInfo.ProductActivityList, RedisKeyConst.GetProductSKUKey(id).Key);
                }
                else
                {
                    DeleteByKey(redisKey);
                }

            }
        }
    }

}
