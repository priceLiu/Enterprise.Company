using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Test.LogicInterface;
using Wcf.Test.LogicInterface.Modules;
using System.ServiceModel;
namespace Wcf.Test.LogicImpl
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class OrderServer:LogicInterface.IOrderService
    {
        public List<LogicInterface.Modules.Order> List()
        {
            List<Order> result = new List<Order>();
            for (int i = 0; i < 20; i++)
            {
                Order order = new Order();
                order.CustomerID = "test";
                order.EmployeeID = "123";
                order.OrderDate = DateTime.Now;
                order.OrderID = "45678";
                order.RequiredDate = DateTime.Now;
                order.ShipAddress = "guangzhou";
                order.ShipCity = "guangzhou";
                order.ShipCountry = "china";
                order.ShipName = "test";
                order.ShipPostalCode = "510520";
                order.ShipRegion = "guangdong";
                result.Add(order);

            }
            return result;
        }
    }
}
