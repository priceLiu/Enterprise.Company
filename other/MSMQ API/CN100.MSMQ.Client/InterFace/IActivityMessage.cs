using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.MSMQ.IAPI
{
    public interface IActivityMessage : ICloneable
    {
        /// <summary>商品活动变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeProductActivity(Int64 productId);
        /// <summary>最低价活动变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeLowPriceActivity(Int64 productId);
        /// <summary>限时打折变更
        /// </summary>
        /// <param name="productId"></param>
        [Obsolete("已过时")]
        void ChangeDiscountActivity(Int64 productId);

        /// <summary>商品团购变更
        /// </summary>
        /// <param name="productId"></param>
        void ChangeTuanActivity(Int64 productId);
        /// <summary>满就送活动变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        void ChangeFullSend(Int64 storeId);
        /// <summary>套餐变更
        /// </summary>
        /// <param name="comboId">套餐ID</param>
        void ChangeComboActivity(Int64 storeId);
    }
}
