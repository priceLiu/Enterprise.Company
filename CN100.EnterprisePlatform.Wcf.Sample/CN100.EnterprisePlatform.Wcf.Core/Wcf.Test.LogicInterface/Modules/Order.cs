using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace Wcf.Test.LogicInterface.Modules
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public string OrderID { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string EmployeeID { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public DateTime RequiredDate { get; set; }
        [DataMember]
        public string ShipName { get; set; }
        [DataMember]
        public string ShipAddress { get; set; }
        [DataMember]
        public string ShipCity { get; set; }
        [DataMember]
        public string ShipRegion { get; set; }
        [DataMember]
        public string ShipPostalCode { get; set; }
        [DataMember]
        public string ShipCountry { get; set; }

    }
}
