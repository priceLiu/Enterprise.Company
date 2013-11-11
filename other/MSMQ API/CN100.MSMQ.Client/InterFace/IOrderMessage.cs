using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.MSMQ.IAPI
{
    /// <summary>订单消息发送器
    /// </summary>
    public interface IOrderMessage : ICloneable
    {
        /// <summary>发布商品
        /// </summary>
        /// <param name="commentId">评论ID</param>
        void AddComment(Int64 productId);
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
        void FinishOrder(string orderCode);
        /// <summary>生成商品购买记录
        /// </summary>
        /// <param name="productId">商品ID</param>
        void CreateBuyRecord(long productid);

        /// <summary>订单回滚
        /// </summary>
        /// <param name="productId">商品ID</param>
        void RollBackOrder(Int64 productId);

        /// <summary>创建订单
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        void CreateOrder(string orderCode);
    }
}
