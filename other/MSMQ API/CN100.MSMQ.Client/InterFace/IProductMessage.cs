using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.MSMQ.IAPI
{
    public interface IProductMessage : ICloneable
    {
        /// <summary>
        /// 发布商品
        /// </summary>
        /// <param name="productId">商品ID</param>
        void PublishProduct(Int64 productId);
        /// <summary>商品修改
        /// </summary>
        /// <param name="productId"></param>
        void ModifyProduct(Int64 productId);
        /// <summary>商品删除
        /// </summary>
        /// <param name="productId"></param>
        void DeleteProduct(Int64 productId);
        /// <summary>商品删除
        /// </summary>
        /// <param name="productId"></param>
        void DeleteProduct(long[] productId);
        /// <summary>商品状态变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeProductStatus(Int64 productId);
        /// <summary>批量商品状态变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeProductStatus(Int64[] productId);
        /// <summary>商品SKU变更
        /// </summary>
        /// <param name="productId">商品ID</param>
        void ChangeProductSKU(Int64 productId);

        /// <summary>商品SKUStock变更
        /// </summary>
        /// <param name="productId">商品ID</param>
        void ChangeProductSKUStock(Int64 productId);

        /// <summary> 商品库存变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeProductStockQty(Int64 productId);

        /// <summary>收藏商品
        /// </summary>
        /// <param name="productId"></param>
        void ChangeCollectProduct(Int64 productId);

    }
}
