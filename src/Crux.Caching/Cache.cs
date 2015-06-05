using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Crux.Caching
{
    public interface ICache
    {
        object Get(string key);

        void Add(string key, object value);

        void Add(string key, object value, IEnumerable<CacheExpirationInfo> expirations);

        void Remove(string key);
    }

    public static class CacheExtensions
    {
        public static CacheVariableBuilder<T> BuildVariable<T>(this ICache cache)
        {
            return new CacheVariableBuilder<T>(cache);
        }
    }

    public class RuntimeCache : ICache
    {
        private readonly MemoryCache _cache;

        public RuntimeCache()
        {
            _cache = MemoryCache.Default;
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Add(string key, object value)
        {
            Add(key, value, new CacheExpirationInfo[] {  });
        }

        public void Add(string key, object value, IEnumerable<CacheExpirationInfo> expirations)
        {
            if (_cache.Contains(key)) {
                Remove(key);
            }

            _cache.Add(key, value, CreatePolicy(expirations));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        private CacheItemPolicy CreatePolicy(IEnumerable<CacheExpirationInfo> expirationConfigs)
        {
            var policy = new CacheItemPolicy();

            foreach (CacheExpirationInfo config in expirationConfigs) {
                switch (config.Type) {
                    case "never":
                        policy.Priority = CacheItemPriority.NotRemovable;
                        break;

                    case "file":
                        if (!string.IsNullOrEmpty(config.Param)) {
                            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new[] { config.Param }));
                        }
                        break;

                    case "absolute":
                        TimeSpan absoluteSpan;
                        if (TimeSpan.TryParse(config.Param, out absoluteSpan)) {
                             policy.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteSpan);
                        }
                        break;

                    case "sliding":
                        TimeSpan slidingSpan;
                        if (TimeSpan.TryParse(config.Param, out slidingSpan)) {
                            policy.SlidingExpiration = slidingSpan;
                        }
                        break;
                }
            }

            return policy;
        }

    }
}