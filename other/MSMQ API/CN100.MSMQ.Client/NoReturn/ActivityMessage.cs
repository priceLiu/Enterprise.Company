using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API.NoReturn
{
    public sealed class ActivityMessage : IActivityMessage
    { 

        /// <summary>商品活动变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeProductActivity(Int64 productId)
        {
            return;
        }
        /// <summary>最低价活动变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeLowPriceActivity(Int64 productId)
        {
            return;
        }
        /// <summary>限时打折变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeDiscountActivity(Int64 productId)
        {
            return;
        }
        /// <summary>商品团购变更
        /// </summary>
        /// <param name="productId"></param>
        public void ChangeTuanActivity(Int64 productId)
        {
            return;
        }
        /// <summary>满就送活动变更
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        public void ChangeFullSend(Int64 storeId)
        {
            return;
        }
        /// <summary>套餐变更
        /// </summary>
        /// <param name="comboId">套餐ID</param>
        public void ChangeComboActivity(Int64 storeId)
        {
            return;
        }

        public object Clone()
        {
            return new ActivityMessage();
        }
    }
}
