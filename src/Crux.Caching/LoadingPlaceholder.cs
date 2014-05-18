using System;
using System.Threading;

namespace Crux.Caching
{
    public class LoadingPlaceholder : ICachePlaceholder
    {
        private readonly ICache _cache;
        private readonly ManualResetEvent _loadingEvent;
        private readonly string _key;
        private readonly int _timeout;

        public LoadingPlaceholder(ICache cache, string key, int timeout)
        {
            // Create the internal event and set to 
            // unsignaled so it will block new threads.
            _loadingEvent = new ManualResetEvent(false);
            _cache = cache;
            _key = key;
            _timeout = timeout;
        }

        public object Value
        {
            get
            {
                // Wait for the already active CacheLoader to finish.
                if (!WaitForLoad()) {
                    // If the cacheloader does not finish before the timeout expires then throw a timeout exception.
                    throw new CachingException(String.Format("Timeout expired waiting for cache loader. Key: {0}", _key));
                }

                // Otherwise cache should be properly popuplated.
                return GetValueFromCache();
            }
        }

        private object GetValueFromCache()
        {
            return _cache.Get(_key).UnwrapPlaceholder();
        }

        public void Set()
        {
            _loadingEvent.Set();
        }

        private bool WaitForLoad()
        {
            return _loadingEvent.WaitOne(_timeout, false);
        }
    }
}