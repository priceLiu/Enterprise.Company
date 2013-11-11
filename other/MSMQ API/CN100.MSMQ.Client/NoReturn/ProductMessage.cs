using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API.NoReturn
{
    /// <summary>
    /// 商品消息发送器
    /// </summary>
    public sealed class ProductMessage : IProductMessage
    {
        /// <summary>
        /// 发布商品
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void PublishProduct(Int64 productId)
        {
            return;
        }

        /// <summary>商品修改
        /// </summary>
        /// <param name="productId"></param>
        public void ModifyProduct(Int64 productId)
        {
            return;
        }
        /// <summary>商品删除
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(Int64 productId)
        {
            return;
        }
        /// <summary>商品删除
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(long[] productId)
        {
            return;
        }
        /// <summary>商品状态变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductStatus(Int64 productId)
        {
            return;
        }
        /// <summary>批量商品状态变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductStatus(Int64[] productId)
        {
            return;
        }
        /// <summary>商品SKU变更
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void ChangeProductSKU(Int64 productId)
        {
            return;
        }

        /// <summary>商品SKUStock变更
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void ChangeProductSKUStock(Int64 productId)
        {
            return;
        }

        /// <summary> 商品库存变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductStockQty(Int64 productId)
        {
            return;
        }

        /// <summary>收藏商品
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeCollectProduct(Int64 productId)
        {
            return;
        }


        public object Clone()
        {
            return new ProductMessage();
        }
    }
}
