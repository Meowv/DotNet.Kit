using System;
using System.Web;
using System.Web.Caching;

namespace DotNet.Kit
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            var cache = HttpRuntime.Cache;
            return cache[cacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        public static void SetCache(string cacheKey, object value)
        {
            var cache = HttpRuntime.Cache;
            cache.Insert(cacheKey, value);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        public static void SetCache(string cacheKey, object value, TimeSpan timeout)
        {
            var cache = HttpRuntime.Cache;
            cache.Insert(cacheKey, value, null, DateTime.MaxValue, timeout, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static void SetCache(string cacheKey, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            var cache = HttpRuntime.Cache;
            cache.Insert(cacheKey, value, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void RemoveCache(string cacheKey)
        {
            var cache = HttpRuntime.Cache;
            cache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveCache()
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}