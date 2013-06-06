using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.ORM.Interface;

namespace CN100.EnterprisePlatform.ORM
{   
    public sealed class ORMCache : ICache,IEnumerable
    {
        private static ORMCache _cache;

        public static readonly ORMCache Instance = new ORMCache();

        private ORMCache()
        {
            _cache = new ORMCache();
        }

        public static ORMCache Cache
        {
            get { return _cache; }
        }

        public object Add(string key, object value, CacheDependency dependencies)
        {
            throw new NotImplementedException();
        }

        public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            throw new NotImplementedException();
        }

        public void Invalidate(IEnumerable<string> entitySets)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key, CacheGetOptions getOptions)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            throw new NotImplementedException();
        }

        public object Remove(string key)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public long EffectivePercentagePhysicalMemoryLimit
        {
            get { throw new NotImplementedException(); }
        }

        public long EffectivePrivateBytesLimit
        {
            get { throw new NotImplementedException(); }
        }

        public object Item
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
