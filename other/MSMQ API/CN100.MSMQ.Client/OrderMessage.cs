using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API
{
    /// <summary>订单消息发送器
    /// </summary>
    public sealed class OrderMessage : Client, IOrderMessage
    {
        /// <summary>发布商品
        /// </summary>
        /// <param name="commentId">评论ID</param>
        public void AddComment(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Order, string.Format(ArgumentFormart, productId.ToString(), ActionEnum.AddComment)));
        }
        /// <summary>订单成功
        /// </summary>
        /// <param name="orderCode">商品ID</param>
        public void FinishOrder(string orderCode)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Order, string.Format(ArgumentFormart, orderCode, ActionEnum.FinishOrder)));
        }
        /// <summary>订单回滚
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void RollBackOrder(Int64 productId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Order, string.Format(ArgumentFormart, productId, ActionEnum.RollBackOrder)));
        }
        /// <summary>生成商品购买记录
        /// </summary>
        /// <param name="productId">商品ID</param>
        public void CreateBuyRecord(long productid)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Order, string.Format(ArgumentFormart, productid, ActionEnum.CreateBuyRecord)));
        }
        /// <summary>创建订单
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        public void CreateOrder(string orderCode)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Order, string.Format(ArgumentFormart, orderCode, ActionEnum.CreateOrder)));
        }

        public object Clone()
        {
            return new OrderMessage();
        }
    }
}
