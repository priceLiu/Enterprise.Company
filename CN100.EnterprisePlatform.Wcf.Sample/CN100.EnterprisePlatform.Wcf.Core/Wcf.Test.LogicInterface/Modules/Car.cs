using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace Wcf.Test.LogicInterface.Modules
{
    [DataContract]
    public class Car
    {
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public CarConditionEnum Condition
        {
            get;
            set;
        }
    }

    [DataContract]
    public enum CarConditionEnum
    {
        [EnumMember]
        New,
        [EnumMember]
        Used,
        [EnumMember]
        Rental,
        Broken,
        Stolen
    }
}
