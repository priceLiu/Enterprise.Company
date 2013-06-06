using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace CN100.EnterprisePlatform.ORM.Interface
{
    public interface ICache
    {

        // Methods
        object Add(string key, object value, CacheDependency dependencies);
        object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
        void Invalidate(IEnumerable<string> entitySets);
        object Get(string key);
        object Get(string key, CacheGetOptions getOptions);
        IDictionaryEnumerator GetEnumerator();
        void Insert(string key, object value);
        void Insert(string key, object value, CacheDependency dependencies);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
        object Remove(string key);

        // Properties
        int Count
        {
            get;
        }
        long EffectivePercentagePhysicalMemoryLimit
        {
            get;
        }
        long EffectivePrivateBytesLimit
        {
            get;
        }
        object Item
        {
            get;
            set;
        }

    }


    public enum CacheGetOptions
    {
        // Fields
        None = 0,
        ReturnCacheEntry = 1
    }
}
