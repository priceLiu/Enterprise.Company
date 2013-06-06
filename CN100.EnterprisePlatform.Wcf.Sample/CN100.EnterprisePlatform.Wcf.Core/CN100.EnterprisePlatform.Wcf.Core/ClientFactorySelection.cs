using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CN100.EnterprisePlatform.Wcf.Core
{
    public class ClientFactorySelection:System.Configuration.ConfigurationSection
    {
        public ClientFactorySelection()
        {
            mConnections = new ConnectionCollection();
           
        }
        private ConnectionCollection mConnections;
        [ConfigurationProperty("Host", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConnectionCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ConnectionCollection Connections
        {

            get
            {
                mConnections =
               (ConnectionCollection)base["Host"];
                return mConnections;
            }
        }
    }
    public class ConnectionCollection : ConfigurationElementCollection
    {
        public ConnectionCollection()
        {

        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionElement)element).IP;
        }

        public ConnectionElement this[int index]
        {
            get
            {
                return (ConnectionElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ConnectionElement this[string Name]
        {
            get
            {
                return (ConnectionElement)BaseGet(Name);
            }
        }

        public int IndexOf(ConnectionElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(ConnectionElement url)
        {
            BaseAdd(url);
        }
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(ConnectionElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.IP);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }

    }
    public class ConnectionElement : ConfigurationElement
    {
        public ConnectionElement(String ip, string port)
        {
            IP = ip;
            Port = port;
        }

        public ConnectionElement()
        {

        }
        [ConfigurationProperty("port")]
        public string Port
        {
            get
            {
                return (string)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }
        [ConfigurationProperty("ip", IsRequired = true, IsKey = true)]
        public string IP
        {
            get
            {
                return (string)this["ip"];
            }
            set
            {
                this["ip"] = value;
            }
        }

    }
}
