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
    /// <summary>
    /// 商品类消息处理
    /// </summary>
    public class ProductHandler : IPlugin
    {
       
        public ModuleEnum Module
        {
            get { return ModuleEnum.Product; }
        }
        /// <summary>ID
        /// </summary>
        public Int64 ID { get; set; }
        ProductService productService = new ProductService();
        ActivitysService activityService = new ActivitysService();
        OrderService orderService = new OrderService();

        public void Process(JsonObject obj)
        {
           
               
                if (obj.IsNull || obj.IsEmpty) return;

                ID = obj["ID"].ToLong(0);

                if (obj["Action"].IsNull || obj["Action"].IsEmpty) return;

                //获取动作类型
                ActionEnum Action = (ActionEnum)Enum.Parse(typeof(ActionEnum), obj["Action"].Value);

                switch (Action)
                {
                    case ActionEnum.ChangeProductSKU:
                        //变更商品SKu
                        productService.ChangeProductSKU(ID);
                        break;
                    case ActionEnum.ChangeProductStatus:
                        //变更商品状态
                        productService.ChangeProductStatus(ID);
                        break;
                    case ActionEnum.ChangeProductActivity:
                        //变更商品活动数据
                        activityService.ChangeProductActivity(ID);
                        break;
                    case ActionEnum.ModifyProduct:
                        productService.ModifyProduct(ID);
                        break;
                    case ActionEnum.DeleteProduct:
                        productService.DeleteProduct(ID);
                        break;
                    case ActionEnum.PublishProduct:                       
                        productService.PublishProduct(ID);
                        orderService.AddComment(ID);
                        break;
                    case ActionEnum.ChangeProductRecommend:
                        ///TODO:暂时不处理橱窗推荐
                        break;
                    case ActionEnum.ChangeProductQty:
                        productService.ChangeProductQty(ID);
                        break;
                    case ActionEnum.ChangeProductTuan:
                        activityService.ChangeProductTuan(ID);
                        break;
                    case ActionEnum.CollectProduct:
                        productService.ChangeProductStatistics(ID);
                        break;
                    case ActionEnum.DeleteProducts:
                        productService.DeleteProduct(obj["ID"].Value);
                        break;
                    case ActionEnum.ChangeProductStatusList:
                        productService.ChangeProductStatusList(obj["ID"].Value);
                        break;
                    default: break;
                }

              
        }

        public ActionEnum Action { get; set; }
    }
}
