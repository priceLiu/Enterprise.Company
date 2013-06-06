using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Wcf.Test.LogicInterface.Modules;
namespace Wcf.Test.LogicInterface
{
    [ServiceContract(Namespace = "http://www.cn100.com", Name = "OrderService")]
    
    public interface IOrderService
    {
        [OperationContract]
        List<Order> List();
        
    }
}
