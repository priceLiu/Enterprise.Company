using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i <= 100000; i++)
            //{
            //    System.Threading.ThreadPool.QueueUserWorkItem(Run);
            //}
            //Console.ReadKey();
            Run();
            //Console.Write("ProductDetail>");
            //var cmd = Console.ReadLine();
            //while (cmd!=null)
            //{

            //}

        }

        static void Run(object state = null)
        {
            //购买记录，评论
            //CN100.MSMQ.IAPI.IOrderMessage oClient = APIFactory.APIFactory.OrderMessage;
            //oClient.AddComment(348479);
            // oClient.FinishOrder("1307000000000344800");
            CN100.MSMQ.IAPI.IStoreMessage sClient = APIFactory.APIFactory.StoreMessage;
            sClient.AuditStore(11916);
            //sClient.AuditStore(5533);
            //sClient.AuditStore(5745);
            //sClient.AuditStore(3703);
            //sClient.ChangeProductCategory(3793);
            //sClient.ChangeProductRecommend(3793);
            //sClient.ChangeStoreCategory(3793);
            //sClient.ChangeStoreCustomService(3793);
            //sClient.ChangeStoreNavMenu(3793);
            //sClient.ChangeStoreRecommend(3793);
            //sClient.ChangePageBackGround(3793);
            //sClient.PublishStore(5745);
            //   sClient.ChangeCollectStore(5745);



            CN100.MSMQ.IAPI.IProductMessage pClient = APIFactory.APIFactory.ProductMessage;
            //   pClient.ChangeProductStatus(new long[] { 379795, 499305 });
            pClient.PublishProduct(460707);
            //pClient.ModifyProduct(243949);
            //pClient.PublishProduct(243949);
            //pClient.PublishProduct(243934);
            //  pClient.DeleteProduct(new long[] { 166527, 166527 });


            ////////pClient.ChangeCollectProduct(166527);
            ////////pClient.ChangeProductStockQty(166527);
            ////////pClient.ChangeProductSKUStock(166527);
            ////////pClient.ChangeProductStatus(166527);   
            //////// pClient.ChangeStoreFreightTemplate(3793);

            //   pClient.ChangeProductSKU(376702);

            // CN100.MSMQ.IAPI.IActivityMessage aClient = APIFactory.APIFactory.ActivityMessage;
            //aClient.ChangeComboActivity(5533);
            //aClient.ChangeComboActivity(5533);
            //     aClient.ChangeFullSend(5533);
            //aClient.ChangeFullSend(5533);
            //    aClient.ChangeProductActivity(348479); 

            CN100.MSMQ.IAPI.IFreightMessage fClient = APIFactory.APIFactory.FreightMessage;
            fClient.ChangeAdressBase();
            fClient.ChangeStoreFreightTemplate(5533);  //(需要debug)

        }
    }
}
