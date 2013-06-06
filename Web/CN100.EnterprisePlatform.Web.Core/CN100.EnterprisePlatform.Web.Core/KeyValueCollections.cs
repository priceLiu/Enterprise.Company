using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
namespace CN100.EnterprisePlatform.Web.Core
{
    public class KeyValueCollection<T> : System.Collections.Specialized.NameObjectCollectionBase
    {


        public KeyValueCollection()
        {
        }


        public KeyValueCollection(IDictionary d, Boolean bReadOnly)
        {
            foreach (DictionaryEntry de in d)
            {
                this.BaseAdd((String)de.Key, de.Value);
            }
            this.IsReadOnly = bReadOnly;
        }


        public DictionaryEntry this[int index]
        {
            get
            {
                return (new DictionaryEntry(
                    this.BaseGetKey(index), this.BaseGet(index)));
            }
        }


        public T this[String key]
        {
            get
            {
                return (T)(this.BaseGet(key));
            }
            set
            {
                this.BaseSet(key, value);
            }
        }


        public String[] AllKeys
        {
            get
            {
                return (this.BaseGetAllKeys());
            }
        }


        public Array AllValues
        {
            get
            {
                return (Array)(this.BaseGetAllValues());
            }
        }


        public Boolean HasKeys
        {
            get
            {
                return (this.BaseHasKeys());
            }
        }

        public void Add(String key, T value)
        {
            this.BaseAdd(key, value);
        }

        public void Remove(String key)
        {
            this.BaseRemove(key);
        }


        public void Remove(int index)
        {
            this.BaseRemoveAt(index);
        }


        public void Clear()
        {
            this.BaseClear();
        }
    }
}
