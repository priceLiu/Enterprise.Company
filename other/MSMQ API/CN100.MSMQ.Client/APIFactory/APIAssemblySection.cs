using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CN100.MSMQ.Configration
{
    public class APIAssemblySection : ConfigurationSection
    {
        // Fields
        private APIAssemblyCollection mAssemblies;

        // Methods
        public APIAssemblySection()
        {
            this.mAssemblies = new APIAssemblyCollection();
        }

     

        // Properties
        [ConfigurationCollection(typeof(APIAssemblyCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove"), ConfigurationProperty("APIAssemblys", IsDefaultCollection = true)]
        public APIAssemblyCollection Assemblies
        {
            get
            {
                this.mAssemblies = (APIAssemblyCollection)this["APIAssemblys"];
                return this.mAssemblies;
            }
        }



    }
}
