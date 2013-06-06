using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Test.LogicInterface;
using Wcf.Test.LogicInterface.Modules;
using CN100.EnterprisePlatform.DAL.Model;
using CN100.EnterprisePlatform.DAL;
using CN100.EnterprisePlatform.BLL.Model;
using Smark.Data;
using CN100.EnterprisePlatform.ORM;
using System.ServiceModel;
namespace Wcf.Test.LogicImpl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UserService:Wcf.Test.LogicInterface.IUserService
    {
        
        public User GetUser(string id)
        {
            User user = new User();
            user.UserID = Guid.NewGuid();
            user.UserName = "henryfan";
            user.Email = "aa@msn.com";
            user.RealName = "testbbqsdkfj sa";
            user.MobilePhone = "13418874567";
            user.LoginEnum = LoginEnum.Success;
            return user;
        }
        public List<User> List_SMARK(int page, int size)
        {  
            UserSingle single = new UserSingle();
            single.Cols = "*";
            single.OrderBy = "createtime";
            single.PageIndex = 100;
            single.PageSize = 20;
            single.TableName = "tbluser";
            single.Where = "";
            List<User> result = (List<User>)DBContext.ExecProcToObjects<User>(single);
            return result;
        }
        public List<User> List_Read(int page, int size)
        {
            List<User> result = new List<User>();
            using (IDb db = DbFactory.Instance.GetDb())
            {
                 
            }
            return result;
        }
        public List<User> List_CN100(int page, int size)
        {
            List<User> result = new List<User>();
            using (IDb db = DbFactory.Instance.GetDb())
            {
                try
                {
                    UserDAL ud = new UserDAL();
                    ud.Db = db;
                    foreach (UserBM bm in ud.GetUserList(10, 20))
                    {
                        User item = new User();
                        item.Email = bm.Email;
                        item.MobilePhone = bm.MobilePhone;
                        item.Password = bm.Password;
                        item.RealName = bm.RealName;
                        item.UserID = bm.UserID;
                        item.UserName = bm.UserName;
                        item.LoginEnum = LoginEnum.Success;
                        result.Add(item);
                    }
                   
                }
                catch (Exception e_)
                {
                    Console.WriteLine(e_.Message);
                    Console.WriteLine(e_.StackTrace);
                }
            }
            return result;
        }
    }
}
