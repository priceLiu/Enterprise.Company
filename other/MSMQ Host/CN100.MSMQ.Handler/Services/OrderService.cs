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
    public class OrderService : BaseService
    {

        ProductService productService;
        public OrderService()
        {

            productService = new ProductService();
        }
        /// <summary>评论新增
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void AddComment(long productId)
        {
            var Key = RedisKeyConst.GetProductCommentsKey(productId).Key;
            var LastItem = CN100.Redis.Client.RedisClientUtility.GetDataListByRange<ProductCommentModel>(Key, 0, 0);
            using (var client = factory.CreateClient<IOrderService>())
            {
                IList<ProductCommentModel> list;
                if (LastItem != null && LastItem.Count > 0)
                {

                    list = client.Channel.GetProductCommentsAfterMinCommentID(productId, LastItem[0].Id);

                }
                else
                {
                    list = client.Channel.GetProductComments(productId);
                }
                list = list.OrderByDescending(it => it.Id).ToList();
                CN100.Redis.Client.RedisClientUtility.SetDataList<ProductCommentModel>(Key, list);
                productService.ChangeProductStatistics(productId);

            }


        }


        /// <summary>订单完成
        /// </summary>
        /// <param name="orderCode"></param>
        public void FinishOrder(string orderCode)
        {
            using (var client = factory.CreateClient<IOrderService>())
            {
                var orderDetailList = client.Channel.GetOrderDetail(orderCode);
                if (orderDetailList != null && orderDetailList.Count > 0)
                {
                    var keys = orderDetailList.Select(it => it.ProductID).Distinct().ToArray();
                    var key = string.Empty;
                    IList<OrderDetailModel> list;
                    foreach (var item in keys)
                    {
                        key = RedisKeyConst.GetProductBuyRecordsKey( item).Key;
                        list = orderDetailList.Where(it => it.ProductID == item).OrderByDescending(it => it.Id).ToList();
                        CN100.Redis.Client.RedisClientUtility.SetDataList<OrderDetailModel>(key, list);
                        productService.ChangeProductStatistics(item);
                        productService.ChangeProductSKU(item);
                    }
                }
            }
        }

        public void CreateBuyRecodByProductId(long ID)
        {
            string key =  RedisKeyConst.GetProductBuyRecordsKey(ID).Key;
            using (var client = factory.CreateClient<IOrderService>())
            {
                var orderDetailList = client.Channel.GetProductBuyRecords(ID);
                CN100.Redis.Client.RedisClientUtility.Del(key);
                CN100.Redis.Client.RedisClientUtility.SetDataList<OrderDetailModel>(key, orderDetailList);
            }
        }

        public void CreateOrder(string OrderCode)
        {
            if (string.IsNullOrWhiteSpace(OrderCode))
            {
                return;
            }
            using (var client = factory.CreateClient<IOrderService>())
            {
                var ids = client.Channel.GetOrderDetailProductId(OrderCode);
                if (ids == null) return;
                foreach (var item in ids)
                {
                    productService.ChangeProductSKU(item);
                }
            }
        }
    }
}
