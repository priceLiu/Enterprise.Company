using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API
{
    /// <summary>
    /// 商品消息发送器
    /// </summary>
    public sealed class ProductMessage : Client, IProductMessage
    {
        /// <summary>
        /// 发布商品
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void PublishProduct(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.PublishProduct)));
        }

        /// <summary>商品修改
        /// </summary>
        /// <param name="productId"></param>
        public void ModifyProduct(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.ModifyProduct)));
        }
        /// <summary>商品删除
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.DeleteProduct)));
        }
        /// <summary>商品删除
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(long[] productIds)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, "\"" + string.Join(",", productIds) + "\"", ActionEnum.DeleteProducts)));
        }
        /// <summary>商品状态变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductStatus(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.ChangeProductStatus)));
        }
        /// <summary>批量商品状态变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductStatus(Int64[] productIds)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, "\"" + string.Join(",", productIds) + "\"", ActionEnum.ChangeProductStatusList)));
        }
        /// <summary>商品SKU变更
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void ChangeProductSKU(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.ChangeProductSKU)));
        }

        /// <summary>商品SKUStock变更
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void ChangeProductSKUStock(Int64 productId)
        {
            ChangeProductSKU(productId);
        }

        /// <summary> 商品库存变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductStockQty(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.ChangeProductQty)));
        }

        /// <summary>收藏商品
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeCollectProduct(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Product, string.Format(ArgumentFormart, productId, ActionEnum.CollectProduct)));
        }


        public object Clone()
        {
            return new ProductMessage();
        }
    }
}
