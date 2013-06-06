using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Wcf.Test.LogicInterface.Modules
{
    [DataContract]
    public enum RegisterEnum
    {
        [EnumMember]
        RegisterSuccess = 1,
        [EnumMember]
        UserNameAlreadyExists = 2,
        [EnumMember]
        RegisterFail = 3
    }

    [DataContract]
    public enum LoginEnum
    {
        [EnumMember]
        NULL = 0,
        [EnumMember]
        Success = 1,
        [EnumMember]
        UserNameNotExist = 2,
        [EnumMember]
        PasswordError = 3,
        [EnumMember]
        Fail = 4,
        [EnumMember]
        Exists = 5
    }
}
