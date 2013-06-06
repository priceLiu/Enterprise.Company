using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smark.Data;
using Smark.Data.Mappings;
namespace Wcf.Test.LogicImpl
{
    [Table("tblUser")]
    public interface IUserSD
    {
        [ID]
        Guid UserID { get; set; }
        [Column]
         string UserName { get; set; }
        [Column]
         string Password { get; set; }
        [Column]
         string RealName { get; set; }
        [Column]
         string Email { get; set; }
        [Column]
         string MobilePhone { get; set; }
     
    }
}
