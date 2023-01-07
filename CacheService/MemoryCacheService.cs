using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace AsthaIslamicService.CacheService
{
    public class MemoryCacheService
    {
        private readonly System.Runtime.Caching.MemoryCache cache = System.Runtime.Caching.MemoryCache.Default;
        private readonly DateTimeOffset InfiniteAbsoluteExpiration = DateTimeOffset.MaxValue;
        private CacheItemPolicy cacheItemPolicy;
        private string keyPostfix = "_Date";
        private object _sync = new object();
        private DateTime ExpireDate = DateTime.Now;
        private DateTime TokenExpireDate = DateTime.Now.AddYears(10);

        public MemoryCacheService()
        {
            _sync = new object();
            ExpireDate = DateTime.Now.AddMonths(12);

             cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = InfiniteAbsoluteExpiration,
            };
        }
        public bool Exists(string key)
        {
            lock (_sync)
            {
                return cache.Contains(key);
            }
        }

        public object Get(string key)
        {
            lock (_sync)
            {
                if (!Exists(key)) throw new ApplicationException(string.Format("An object with key '{0}' does not exists", key));
                lock (_sync)
                {
                    return cache[key];
                }
            }
        }

        public void Add(string key, object value)
        {
            lock (_sync)
            {
                if (Exists(key))
                {
                    lock (_sync)
                    {
                        cache.Add(key, value, cacheItemPolicy);
                        cache.Add(key + keyPostfix, ExpireDate, cacheItemPolicy);
                    }
                }
                else
                {
                    throw new ApplicationException(string.Format("An object with key '{0}' already exists", key));
                }
            }
        }

        public object Set(string key, object value)
        {
            lock (_sync)
            {
                if (!Exists(key))
                {
                    lock (_sync)
                    {
                        cache.Add(key, value, cacheItemPolicy);
                        cache.Add(key + keyPostfix, ExpireDate, cacheItemPolicy);
                    }
                }
                else
                {
                    lock (_sync)
                    {
                        cache.Set(key, value, cacheItemPolicy);
                        cache.Set(key + keyPostfix, ExpireDate, cacheItemPolicy);
                    }
                }
                lock (_sync)
                {
                    return cache[key];
                }

            }
        }

        public object SetInfinite(string key, object value)
        {
            lock (_sync)
            {
                if (!Exists(key))
                {
                    lock (_sync)
                    {
                        cache.Add(key, value, cacheItemPolicy);
                        cache.Add(key + keyPostfix, TokenExpireDate, cacheItemPolicy);
                    }
                }
                else
                {
                    lock (_sync)
                    {
                        cache.Set(key, value, cacheItemPolicy);
                        cache.Set(key + keyPostfix, TokenExpireDate, cacheItemPolicy);
                    }
                }
                lock (_sync)
                {
                    return cache[key];
                }

            }
        }

        public bool RemoveAllCacheByKey(string startskey)
        {
            try
            {
                var keyList = cache.Where(x => x.Key.Contains(startskey)).Select(x => x.Key).ToList();
                foreach (var key in keyList)
                {
                    lock (_sync)
                        cache.Remove(key);
                }
            }
            catch { }

            return true;
        }

        public bool IfExpireThenSet(string key)
        {
            lock (_sync)
            {
                if (!Exists(key)) throw new ApplicationException(string.Format("An object with key '{0}' does not exists", key));
                DateTime MemCacheExpiryDate = Convert.ToDateTime(cache.Get(key + keyPostfix));
                if (DateTime.Compare(DateTime.Now, MemCacheExpiryDate) > 0)
                {
                    //lock (_sync)
                    //{
                    //    Set(key, value);
                    //}
                    return true;
                }
                return false;
            }
        }

        public void Remove(string key)
        {
            lock (_sync)
            {
                if (!Exists(key)) throw new ApplicationException(string.Format("An object with key '{0}' does not exists in cache", key));
                lock (_sync)
                {
                    cache.Remove(key);
                }
            }
        }
    }
}
