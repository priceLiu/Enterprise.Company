using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Wcf.Test.LogicInterface.Modules;
namespace Wcf.Test.LogicInterface
{
    [ServiceContract(Namespace = "http://www.cn100.com", Name = "UserService")]

    public interface IUserService
    {
        [OperationContract]
        User GetUser(string id);
        [OperationContract]
        List<User> List_SMARK(int page, int size);
        [OperationContract]
        List<User> List_CN100(int page, int size);
    }
}
