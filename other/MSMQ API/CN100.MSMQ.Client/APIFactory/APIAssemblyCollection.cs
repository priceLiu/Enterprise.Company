using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CN100.MSMQ.Configration
{
    public class APIAssemblyCollection : ConfigurationElementCollection
    {
        // Methods
        public void Add(APIAssemblyElement api)
        {
            this.BaseAdd(api);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }

        public void Clear()
        {
            base.BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new APIAssemblyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((APIAssemblyElement)element).Name;
        }

        public int IndexOf(APIAssemblyElement api)
        {
            return base.BaseIndexOf(api);
        }

        public void Remove(APIAssemblyElement api)
        {
            if (base.BaseIndexOf(api) >= 0)
            {
                base.BaseRemove(api.Name);
            }
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            base.BaseRemoveAt(index);
        }

        // Properties
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public APIAssemblyElement this[string Name]
        {
            get
            {
                return (APIAssemblyElement)base.BaseGet(Name);
            }
        }

        public APIAssemblyElement this[int index]
        {
            get
            {
                return (APIAssemblyElement)base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

    }
}
