using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CN100.MSMQ.Configration
{
    public class APIAssemblyElement : ConfigurationElement
    {

        // Methods
        public APIAssemblyElement()
        {
        }
 

        // Properties
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["Name"];
            }
            set
            {
                base["Name"] = value;
            }
        }
        [ConfigurationProperty("DLLName", IsRequired = true, IsKey = false)]
        public string DLLName
        {
            get
            {
                return (string)base["DLLName"];
            }
            set
            {
                base["DLLName"] = value;
            }
        }
        [ConfigurationProperty("FullClassName", IsRequired = true, IsKey = false)]
        public string FullClassName
        {
            get
            {
                return (string)base["FullClassName"];
            }
            set
            {
                base["FullClassName"] = value;
            }
        }


    }
}
