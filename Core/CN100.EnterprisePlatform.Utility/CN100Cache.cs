using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.EnterprisePlatform.Cache;

namespace CN100.EnterprisePlatform.Utility
{
    public static class CN100Cache
    {
        #region 构造函数，初始化分布式缓存对象，设置缓存默认参数

        static MemcachedClient cache = null;

        static CN100Cache()
        {
            cache = MemcachedClient.GetInstance("CN100Cache");


            cache.KeyPrefix = "";//缓存键前缀

            cache.SendReceiveTimeout = 2000;//客户端接收返回值超时时间

            cache.ConnectTimeout = 2000;//客户端连接服务器超时时间

            cache.MinPoolSize = 5;//缓存池最小连接数

            cache.MaxPoolSize = 2000;//缓存池最大连接数

            cache.SocketRecycleAge = TimeSpan.FromMinutes(180);//缓存默认过期时间

            cache.CompressionThreshold = 1024 * 128;//缓存

        }
        #endregion

        #region Set设置缓存
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <returns></returns>
        public static bool Set(string strKey,object objValue)
        {
            return cache.Set(strKey, objValue);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Set(string strKey,object objValue,DateTime dtExpiry)
        {
            return cache.Set(strKey, objValue, dtExpiry);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Set(string strKey, object objValue, TimeSpan tsExpiry)
        {
            return cache.Set(strKey, objValue, tsExpiry);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <returns></returns>
        public static bool Set(string strKey,object objValue,uint uintHash)
        {
            return cache.Set(strKey, objValue, uintHash);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Set(string strKey,object objValue,uint uintHash,DateTime dtExpiry)
        {
            return cache.Set(strKey, objValue, uintHash,dtExpiry);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Set(string strKey, object objValue, uint uintHash, TimeSpan tsExpiry)
        {
            return cache.Set(strKey, objValue, uintHash, tsExpiry);
        }
        #endregion

        #region Add新增缓存
        /// <summary>
        /// 新增缓存，缓存键必须未存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <returns></returns>
        public static bool Add(string strKey, object objValue)
        {
            return cache.Add(strKey, objValue);
        }
        /// <summary>
        /// 新增缓存，缓存键必须未存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Add(string strKey, object objValue, DateTime dtExpiry)
        {
            return cache.Add(strKey, objValue, dtExpiry);
        }
        /// <summary>
        /// 新增缓存，缓存键必须未存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Add(string strKey, object objValue, TimeSpan tsExpiry)
        {
            return cache.Add(strKey, objValue, tsExpiry);
        }
        /// <summary>
        /// 新增缓存，缓存键必须未存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <returns></returns>
        public static bool Add(string strKey, object objValue, uint uintHash)
        {
            return cache.Add(strKey, objValue, uintHash);
        }
        /// <summary>
        /// 新增缓存，缓存键必须未存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Add(string strKey, object objValue, uint uintHash, DateTime dtExpiry)
        {
            return cache.Add(strKey, objValue, uintHash, dtExpiry);
        }
        /// <summary>
        /// 新增缓存，缓存键必须未存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Add(string strKey, object objValue, uint uintHash, TimeSpan tsExpiry)
        {
            return cache.Add(strKey, objValue, uintHash, tsExpiry);
        }
        #endregion

        #region Replace更新缓存
        /// <summary>
        /// 更新缓存，缓存键必须已存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <returns></returns>
        public static bool Replace(string strKey, object objValue)
        {
            return cache.Replace(strKey, objValue);
        }
        /// <summary>
        /// 更新缓存，缓存键必须已存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Replace(string strKey, object objValue, DateTime dtExpiry)
        {
            return cache.Replace(strKey, objValue, dtExpiry);
        }
        /// <summary>
        /// 更新缓存，缓存键必须已存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Replace(string strKey, object objValue, TimeSpan tsExpiry)
        {
            return cache.Replace(strKey, objValue, tsExpiry);
        }
        /// <summary>
        /// 更新缓存，缓存键必须已存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <returns></returns>
        public static bool Replace(string strKey, object objValue, uint uintHash)
        {
            return cache.Replace(strKey, objValue, uintHash);
        }
        /// <summary>
        /// 更新缓存，缓存键必须已存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Replace(string strKey, object objValue, uint uintHash, DateTime dtExpiry)
        {
            return cache.Replace(strKey, objValue, uintHash, dtExpiry);
        }
        /// <summary>
        /// 更新缓存，缓存键必须已存在
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Replace(string strKey, object objValue, uint uintHash, TimeSpan tsExpiry)
        {
            return cache.Replace(strKey, objValue, uintHash, tsExpiry);
        }
        #endregion

        #region Append 在已有对象后追加内容
        /// <summary>
        /// 在已有对象后追加内容
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存内容</param>
        /// <returns></returns>
        public static bool Append(string strKey,object objValue)
        {
            return cache.Append(strKey, objValue);
        }
        /// <summary>
        /// 在已有对象后追加内容
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存内容</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static bool Append(string strKey, object objValue, uint uintHash)
        {
            return cache.Append(strKey, objValue, uintHash);
        }
        #endregion

        #region Prepend 在已有对象后追加内容
        /// <summary>
        /// 在已有对象前插入内容
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存内容</param>
        /// <returns></returns>
        public static bool Prepend(string strKey, object objValue)
        {
            return cache.Prepend(strKey, objValue);
        }
        /// <summary>
        /// 在已有对象前插入内容
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存内容</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static bool Prepend(string strKey, object objValue, uint uintHash)
        {
            return cache.Prepend(strKey, objValue, uintHash);
        }
        #endregion

        #region CheckAndSet 设置缓存
        /// <summary>
        /// 验证版本号并设置缓存，指定版本不是最新版本，则不设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static string CheckAndSet(string strKey, object objValue, ulong ulongNique)
        {
            return cache.CheckAndSet(strKey, objValue, ulongNique);
        }
        /// <summary>
        /// 验证版本号并设置缓存，指定版本不是最新版本，则不设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static string CheckAndSet(string strKey, object objValue, DateTime dtExpiry, ulong ulongNique)
        {
            return cache.CheckAndSet(strKey, objValue, dtExpiry, ulongNique);
        }
        /// <summary>
        /// 验证版本号并设置缓存，指定版本不是最新版本，则不设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static string CheckAndSet(string strKey, object objValue, TimeSpan tsExpiry, ulong ulongNique)
        {
            return cache.CheckAndSet(strKey, objValue, tsExpiry, ulongNique);
        }
        /// <summary>
        /// 验证版本号并设置缓存，指定版本不是最新版本，则不设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static string CheckAndSet(string strKey, object objValue, uint uintHash, ulong ulongNique)
        {
            return cache.CheckAndSet(strKey, objValue, uintHash, ulongNique);
        }
        /// <summary>
        /// 验证版本号并设置缓存，指定版本不是最新版本，则不设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static string CheckAndSet(string strKey, object objValue, uint uintHash, DateTime dtExpiry, ulong ulongNique)
        {
            return cache.CheckAndSet(strKey, objValue, uintHash, dtExpiry, ulongNique);
        }
        /// <summary>
        /// 验证版本号并设置缓存，指定版本不是最新版本，则不设置缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="objValue">缓存值</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static string CheckAndSet(string strKey, object objValue, uint uintHash, TimeSpan tsExpiry, ulong ulongNique)
        {
            return cache.CheckAndSet(strKey, objValue, uintHash, tsExpiry, ulongNique);
        }
        #endregion

        #region Get获取缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <returns></returns>
        public static object Get(string strKey)
        {
            return cache.Get(strKey);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static object Get(string strKey,uint uintHash)
        {
            return cache.Get(strKey, uintHash);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="strKeyList">缓存键数组</param>
        /// <returns></returns>
        public static object[] Get(string[] strKeyList)
        {
            return cache.Get(strKeyList);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="strKeyList">缓存键数组</param>
        /// <param name="uintHashList">自定义hash数组</param>
        /// <returns></returns>
        public static object[] Get(string[] strKeyList, uint[] uintHashList)
        {
            return cache.Get(strKeyList, uintHashList);
        }
        #endregion

        #region Gets获取缓存并传出版本号
        /// <summary>
        /// 获取缓存并传出版本号
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static object Gets(string strKey,out ulong ulongNique)
        {
            return cache.Gets(strKey,out ulongNique);
        }
        /// <summary>
        /// 获取缓存并传出版本号
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="uintHash">自定义hash</param>
        /// <param name="ulongNique">版本号</param>
        /// <returns></returns>
        public static object Gets(string strKey,uint uintHash, out ulong ulongNique)
        {
            return cache.Gets(strKey,uintHash,out ulongNique);
        }
        /// <summary>
        /// 获取缓存并传出版本号
        /// </summary>
        /// <param name="strKeyList">缓存键数组</param>
        /// <param name="ulongNiqueList">版本号数组</param>
        /// <returns></returns>
        public static object[] Gets(string[] strKeyList, out ulong[] ulongNiqueList)
        {
            return cache.Gets(strKeyList, out ulongNiqueList);
        }
        /// <summary>
        /// 获取缓存并传出版本号
        /// </summary>
        /// <param name="strKeyList">缓存键数组</param>
        /// <param name="uintHashList">自定义hash数组</param>
        /// <param name="ulongNiqueList">版本号数组</param>
        /// <returns></returns>
        public static object[] Gets(string[] strKeyList, uint[] uintHashList, out ulong[] ulongNiqueList)
        {
            return cache.Gets(strKeyList, uintHashList, out ulongNiqueList);
        }
        #endregion

        #region Delete 删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <returns></returns>
        public static bool Delete(string strKey)
        {
            return cache.Delete(strKey);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Delete(string strKey, DateTime dtExpiry)
        {
            return cache.Delete(strKey, dtExpiry);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Delete(string strKey, TimeSpan tsExpiry)
        {
            return cache.Delete(strKey, tsExpiry);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <returns></returns>
        public static bool Delete(string strKey, uint uintHash)
        {
            return cache.Delete(strKey,  uintHash);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool Delete(string strKey, uint uintHash, DateTime dtExpiry)
        {
            return cache.Delete(strKey, uintHash, dtExpiry);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="strKey">缓存键</param>
        /// <param name="uintHash">自定义hash值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool Delete(string strKey, uint uintHash, TimeSpan tsExpiry)
        {
            return cache.Delete(strKey,  uintHash, tsExpiry);
        }
        #endregion

        #region 计数器

        #region 设置缓存计数器
        /// <summary>
        /// 设置缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">值</param>
        /// <returns></returns>
        public static bool SetCounter(string strKey,ulong ulongCount)
        {
            return cache.SetCounter(strKey, ulongCount);
        }
        /// <summary>
        /// 设置缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">值</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool SetCounter(string strKey, ulong ulongCount,DateTime dtExpiry)
        {
            return cache.SetCounter(strKey, ulongCount,dtExpiry);
        }
        /// <summary>
        /// 设置缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">值</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool SetCounter(string strKey, ulong ulongCount, TimeSpan tsExpiry)
        {
            return cache.SetCounter(strKey, ulongCount, tsExpiry);
        }
        /// <summary>
        /// 设置缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">值</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static bool SetCounter(string strKey, ulong ulongCount,uint uintHash)
        {
            return cache.SetCounter(strKey, ulongCount, uintHash);
        }
        /// <summary>
        /// 设置缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">值</param>
        /// <param name="uintHash">自定义hash</param>
        /// <param name="dtExpiry">过期时间</param>
        /// <returns></returns>
        public static bool SetCounter(string strKey, ulong ulongCount, uint uintHash, DateTime dtExpiry)
        {
            return cache.SetCounter(strKey, ulongCount, uintHash, dtExpiry);
        }
        /// <summary>
        /// 设置缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">值</param>
        /// <param name="uintHash">自定义hash</param>
        /// <param name="tsExpiry">过期时间间隔</param>
        /// <returns></returns>
        public static bool SetCounter(string strKey, ulong ulongCount, uint uintHash, TimeSpan tsExpiry)
        {
            return cache.SetCounter(strKey, ulongCount, uintHash, tsExpiry);
        }
        #endregion

        #region 获取缓存计数器
        /// <summary>
        /// 获取缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <returns></returns>
        public static ulong? GetCounter(string strKey)
        {
            return cache.GetCounter(strKey);
        }
        /// <summary>
        /// 获取缓存计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static ulong? GetCounter(string strKey,uint uintHash)
        {
            return cache.GetCounter(strKey,uintHash);
        }
        /// <summary>
        /// 获取缓存计数器
        /// </summary>
        /// <param name="strKeyList">键数组</param>
        /// <returns></returns>
        public static ulong?[] GetCounter(string[] strKeyList)
        {
            return cache.GetCounter(strKeyList);
        }
        /// <summary>
        /// 获取缓存计数器
        /// </summary>
        /// <param name="strKeyList">键数组</param>
        /// <param name="uintHashList">自定义hash数组</param>
        /// <returns></returns>
        public static ulong?[] GetCounter(string[] strKeyList, uint[] uintHashList)
        {
            return cache.GetCounter(strKeyList, uintHashList);
        }
        #endregion

        #region Increment 累加计数器
        /// <summary>
        /// 累加计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">累加值</param>
        /// <returns></returns>
        public static ulong? Increment(string strKey,ulong ulongCount)
        {
            return cache.Increment(strKey, ulongCount);
        }
        /// <summary>
        /// 累加计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">累加值</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static ulong? Increment(string strKey, ulong ulongCount,uint uintHash)
        {
            return cache.Increment(strKey, ulongCount, uintHash);
        }
        /// <summary>
        /// 累加计数器，加1
        /// </summary>
        /// <param name="strKey">键</param>
        /// <returns></returns>
        public static ulong? Increment(string strKey)
        {
            return Increment(strKey, 1);
        }
        /// <summary>
        /// 累加计数器，加1
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static ulong? Increment(string strKey, uint uintHash)
        {
            return Increment(strKey, 1, uintHash);
        } 
        #endregion

        #region Decrement 累减计数器
        /// <summary>
        /// 累减计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">累减值</param>
        /// <returns></returns>
        public static ulong? Decrement(string strKey, ulong ulongCount)
        {
            return cache.Decrement(strKey, ulongCount);
        }
        /// <summary>
        /// 累减计数器
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="ulongCount">累减值</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static ulong? Decrement(string strKey, ulong ulongCount, uint uintHash)
        {
            return cache.Decrement(strKey, ulongCount, uintHash);
        }
        /// <summary>
        /// 累减计数器，减1
        /// </summary>
        /// <param name="strKey">键</param>
        /// <returns></returns>
        public static ulong? Decrement(string strKey)
        {
            return Decrement(strKey, 1);
        }
        /// <summary>
        /// 累减计数器，减1
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static ulong? Decrement(string strKey, uint uintHash)
        {
            return Decrement(strKey, 1, uintHash);
        }
        #endregion

        #endregion

        #region FlushAll 清空缓存
        /// <summary>
        /// 清空缓存，立即清空
        /// </summary>
        /// <returns></returns>
        public static bool FlushAll()
        { 
            return cache.FlushAll();
        }
        /// <summary>
        /// 清空缓存，延时清空
        /// </summary>
        /// <param name="tsDelay">分布式服务器缓存清空延时</param>
        /// <returns></returns>
        public static bool FlushAll(TimeSpan tsDelay)
        {
            return cache.FlushAll(tsDelay);
        }
        /// <summary>
        /// 清空缓存，延时情况
        /// </summary>
        /// <param name="tsDelay">分布式服务器缓存清空延时</param>
        /// <param name="staggered">
        /// true：在每次到达延时时间时，清空一台服务器缓存
        /// false:第一次到达延时时间是，同步清空所有服务器缓存
        /// </param>
        /// <returns></returns>
        public static bool FlushAll(TimeSpan tsDelay, bool staggered)
        {
            return cache.FlushAll(tsDelay, staggered);
        }
        #endregion

        #region 服务器状态 
        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,Dictionary<string,string>> Stats()
        {
            return cache.Stats();
        }
        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <param name="strKey">键</param>
        /// <returns></returns>
        public static Dictionary<string, string> Stats(string strKey)
        {
            return cache.Stats(strKey);
        }
        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <param name="uintHash">自定义hash</param>
        /// <returns></returns>
        public static Dictionary<string, string> Stats(uint uintHash)
        {
            return cache.Stats(uintHash);
        }
        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <param name="strHost">服务器链接</param>
        /// <returns></returns>
        public static Dictionary<string, string> StatsByHost(string strHost)
        {
            return cache.StatsByHost(strHost);
        }

        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> Status()
        {
            return cache.Status();
        }
        #endregion
    }
}
