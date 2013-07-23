using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

using Pms.Common.Variable;

namespace Pms.Common.Helper
{
    public class CacheManager
    {
        /// <summary>
        /// Gets the appropriate cache based on the application(Web/Windows/Console)
        /// </summary>
        private static Cache Cache
        {
            get
            {
                return (HttpContext.Current == null) ? HttpRuntime.Cache : HttpContext.Current.Cache;
            }
        }

        /// <summary>
        /// Method to get the cache object
        /// </summary>
        /// <param name="key">The key parameter.</param>
        /// <returns>returns an Object.</returns>
        public static object Get(string key)
        {
            return Cache[key];
        }

        /// <summary>
        /// Method to get the cache object
        /// </summary>
        /// <typeparam name="T">generic object</typeparam>
        /// <param name="key">The key parameter.</param>
        /// <returns>returns a generic object.</returns>
        public static T Get<T>(string key)
        {
            if (Cache[key] is T)
            {
                return (T)Cache[key];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Method to add into the cache
        /// </summary>
        /// <param name="key">cache key name parameter.</param>
        /// <param name="value">The value parameter.</param>
        /// <param name="cacheTimeout">time span to be cached parameter.</param>
        public static void Add(string key, object value, int cacheTimeout)
        {
            if (value != null)
            {
                HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(GetCacheTimeOut()), Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// Method to add into the cache
        /// </summary>
        /// <param name="key">cache key name parameter.</param>
        /// <param name="value">The value parameter.</param>
        public static void Add(string key, object value)
        {
            Add(key, value, GetCacheTimeOut());
        }

        /// <summary>
        /// Method to invalidate cache based on a key
        /// </summary>
        /// <param name="key">The key parameter.</param>
        public static void Invalidate(string key)
        {
            Cache.Remove(key);
        }


        #region private procedures
        private static int GetCacheTimeOut()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(ConfigurationKey.CacheTimeOut))
            {
                return int.Parse(ConfigurationManager.AppSettings[ConfigurationKey.CacheTimeOut]);
            }
            else
            {
                throw new Exception(Variable.ErrorMessage.ConfigurationMissing);
            }
        }
        #endregion
    }
}
