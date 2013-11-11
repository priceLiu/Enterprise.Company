using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common;
using CN100.ProductDetail.Common.Enums;
using LH.JsonHelper;
using CN100.MSMQ.Handler.Services;

namespace CN100.MSMQ.Handler
{
    public class OrderHandler : IPlugin
    {
        public ModuleEnum Module
        {
            get { return ModuleEnum.Order; }
        }

        Int64 ID { get; set; }
        string OrderCode { get; set; }
        private OrderService orderService = new OrderService();

        public void Process(JsonObject obj)
        {
            if (obj.IsNull || obj.IsEmpty) return;





            if (obj["Action"].IsNull || obj["Action"].IsEmpty) return;

            //获取动作类型
            ActionEnum Action = (ActionEnum)Enum.Parse(typeof(ActionEnum), obj["Action"].Value);

            switch (Action)
            {
                case ActionEnum.AddComment:
                    ID = obj["ID"].ToLong(0);  
                    orderService.AddComment(ID);
                    break;
                case ActionEnum.FinishOrder:
                    OrderCode = obj["ID"].Value;
                    orderService.FinishOrder(OrderCode);
                    break;
                case ActionEnum.CreateBuyRecord:
                        ID = obj["ID"].ToLong(0);  
                    orderService.CreateBuyRecodByProductId(ID);
                    break;
                case ActionEnum.CreateOrder:
                    OrderCode = obj["ID"].Value;
                    orderService.CreateOrder(OrderCode);
                    break;
                    
                default:
                    return;

            }
        }


        public ActionEnum Action
        {
            get;
            set;
        }
    }
}
