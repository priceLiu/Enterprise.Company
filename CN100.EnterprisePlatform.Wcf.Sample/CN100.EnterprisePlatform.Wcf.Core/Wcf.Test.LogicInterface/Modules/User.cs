using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace Wcf.Test.LogicInterface.Modules
{
    [DataContract]
    public class User
    {
        [DataMember]
        public Guid UserID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string RealName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string MobilePhone { get; set; }

        [DataMember]
        public LoginEnum LoginEnum { get; set; }
    }
}
