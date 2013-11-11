using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API.NoReturn
{
    /// <summary>订单消息发送器
    /// </summary>
    public sealed class OrderMessage :  IOrderMessage
    {
        /// <summary>发布商品
        /// </summary>
        /// <param name="commentId">评论ID</param>
        public void AddComment(Int64 productId)
        {
            return;
        }
        /// <summary>发布商品
        /// </summary>
        /// <param name="commentId">评论ID</param>
        //public void DeleteComment(Int64 commentId)
        //{
        //    SendRequest(string.Format(ConstClass.CommandFormart, CommandEnum.DeleteComment, string.Format(ArugmentFormart_Comment, commentId)));
        //}
        /// <summary>订单成功
        /// </summary>
        /// <param name="orderId">商品ID</param>
        public void FinishOrder(string orderCode)
        {
            return;
        }

        /// <summary>订单回滚
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void RollBackOrder(Int64 productId)
        {
            return;
        }

        /// <summary>生成商品购买记录
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void CreateBuyRecord(long productid)
        {
            return;
        }
        /// <summary>创建订单
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        public void CreateOrder(string orderCode)
        {
            return;
        }

        public object Clone()
        {
            return new OrderMessage();
        }
    }
}
