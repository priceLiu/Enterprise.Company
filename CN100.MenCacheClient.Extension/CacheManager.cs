using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Memcached.ClientLibrary;
namespace CN100.MenCacheClient.Extension
{
    public class CacheManager:IDisposable
    {
        private SockIOPool mPool;

        private bool mIsDisposed = false;

        private MemcachedClient mCacheClient;

        private Dictionary<string, List<string>> mKeyRelation = new Dictionary<string, List<string>>();

        public CacheManager(params string[] hostring)
        {
            string poolName = Guid.NewGuid().ToString("N");
            mPool = SockIOPool.GetInstance(poolName);
            mPool.SetServers(hostring);

            mPool.InitConnections = 5;
            mPool.MinConnections = 5;
            mPool.MaxConnections = 10;

            mPool.SocketConnectTimeout = 1000;
            mPool.SocketTimeout = 3000;

            mPool.MaintenanceSleep = 30;
            mPool.Failover = true;

            mPool.Nagle = false;
            mPool.Initialize();
            mCacheClient = new MemcachedClient();
            mCacheClient.PoolName = poolName;
            mCacheClient.EnableCompression = false;
        }

        public bool Initialized
        {
            get
            {
                return mPool.Initialized;
            }
        }

        public void RegRelation(string key, params string[] rkeys)
        {
            lock (mKeyRelation)
            {

            }
        }

        private List<string> GetRelation(string key)
        {
            List<string> result =null;
            lock (mKeyRelation)
            {
                if (!mKeyRelation.TryGetValue(key, out result))
                {
                    result = new List<string>();
                    mKeyRelation.Add(key, result);
                }
            }
            return result;
        }

        public object Get(string key)
        {
            if (mCacheClient.KeyExists(key))
                return mCacheClient.Get(key);
            return null;
        }

        public  void Set(string key, object obj)
        {

            Set(key, obj, DateTime.Now.AddMinutes(10));
        }

        public  void Set(string key, object obj,DateTime expiry)
        {
            if (mCacheClient.KeyExists(key))
                mCacheClient.Replace(key, obj, expiry);
            else
                mCacheClient.Set(key, obj, expiry);
            List<string> result = GetRelation(key);
            for (int i = 0; i < result.Count; i++)
            {
                Delete(result[i]);
            }
        }

        public void Delete(string key)
        {
            
            mCacheClient.Delete(key);
        }

        public void LoadRelation(string xmlfile)
        {

        }

        protected void OnDisposed()
        {
            mPool.Shutdown();
        }

        public void Dispose()
        {
            lock (this)
            {
                if (!mIsDisposed)
                {
                    mIsDisposed = true;
                    OnDisposed();
                }
            }
        }
    }
}
