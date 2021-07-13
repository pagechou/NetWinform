using App.Core.Init;
using App.Core.UseAge;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Core.Caching
{
    internal class MemoryCache : InitIoc,ICache
    {
        public MemoryCache()
        {

        }
        /// <summary>
        /// 清空所有的缓存
        /// </summary> 
        public void Clear(string key = null)
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key != null)
                {
                    if ((key == null || key.Equals(enumerator.Key)))
                    {
                        HttpRuntime.Cache.Remove(enumerator.Key.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 获取一个缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="factory">初始化缓存表达式</param>
        /// <param name="seconds">默认缓存时间，单位秒</param>
        /// <returns>缓存值</returns>
        public object Get(string key, Func<string, object> factory = null, int seconds = 7200)
        {
            if (GetKeys().Contains(key))
            {
                return HttpRuntime.Cache.Get(key);
            }

            if (factory != null)
            {
                HttpRuntime.Cache.Insert(key, factory(key), null, DateTime.Now.AddSeconds((double)seconds), TimeSpan.Zero);
            }
            return HttpRuntime.Cache.Get(key);
        }
        /// <summary>
        /// 获取类型缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">名称</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (GetKeys().Contains(key))
            {
                var item = HttpRuntime.Cache.Get(key);

                if (item == null)
                    return default(T);
                try
                {

                    return (T)item;

                }
                catch
                {

                }

            }
            return default(T);



        }
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetItem(string key)
        {
            if (GetKeys().Contains(key))
            {

                var cacheitem = HttpRuntime.Cache.Get(key);
                if (cacheitem is CacheItem)
                {
                    var c1 = cacheitem as CacheItem;
                    if (c1 == null || c1.Expir < DateTime.Now)
                    { return null; }
                    else
                    {
                        return c1.Data;
                    }
                }
                else
                {
                    return HttpRuntime.Cache.Get(key);
                }

            }

            return null;
        }
        /// <summary>
        /// 获取所有的缓存键
        /// </summary> 
        public List<string> GetKeys()
        {
            List<string> strs = new List<string>();
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key != null)
                {
                    strs.Add(enumerator.Key.ToString());
                }

            }
            return strs;
        }
        /// <summary>
        /// 移除指定的缓存对象
        /// </summary> 
        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
        /// <summary>
        /// 设置一个缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="slidingExpireSeconds">多少秒后过期</param>
        public void SetWithSeconds(string key, object value, int slidingExpireSeconds = 28800)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds(slidingExpireSeconds), TimeSpan.Zero);
        }
        /// <summary>
        /// 设置一个缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="slidingExpireMins"></param>
        public void SetWithMins(string key, object value, int slidingExpireMins = 60)
        {
            CacheItem cacheItem = new CacheItem();
            cacheItem.CacheTimes = slidingExpireMins;
            cacheItem.Key = key;
            cacheItem.Data = value;
            cacheItem.Expir = DateTime.Now.AddMinutes(slidingExpireMins);

            HttpRuntime.Cache.Insert(key, cacheItem, null, cacheItem.Expir, TimeSpan.Zero);
        }
    }
}
