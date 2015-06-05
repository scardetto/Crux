using System;
using System.Collections.Generic;
using Crux.Caching.Logging;

namespace Crux.Caching
{
    public class CacheVariable<T>
    {
        private readonly ICache _cache;
        private readonly ILog _log;
        private readonly IEnumerable<CacheExpirationInfo> _expirationConfigs;

        public bool LazyLoad { get; private set; }
        public int InitializationTimeout { get; private set; }
        public CacheInitializer<T> Initializer { get; private set; }
        public string Key { get; private set; }

        public CacheVariable(ICache cache, string key, CacheInitializer<T> initializer, IEnumerable<CacheExpirationInfo> expirationConfigs, int timeout, bool lazyLoad)
        {
            _cache = cache;
            _log = LogProvider.GetCurrentClassLogger();

            Key = key;
            Initializer = initializer;
            InitializationTimeout = timeout;
            LazyLoad = lazyLoad;
            _expirationConfigs = expirationConfigs;

            if (!lazyLoad) {
                GetOrLoadValue();
            }
        }

        private object GetOrLoadValue()
        {
            var value = GetCachedItem() ?? LoadItem();
            return value.UnwrapPlaceholder();
        }

        private object GetValue()
        {
            var value = GetCachedItem();
            return value.UnwrapPlaceholder();
        }

        private object GetCachedItem()
        {
            return _cache.Get(Key);
        }

        private object LoadItem()
        {
            // Insert a stub into the cache for other threads to sych on.
            var loadingPlaceHolder = new LoadingPlaceholder(_cache, Key, InitializationTimeout);
            _cache.Add(Key, loadingPlaceHolder);

            try {
                var value = Initializer.Initialize();
                AddValueToCache(value);
                return value;
            } catch (Exception e) {
                _log.WarnException("Error loading cache variable", e);
                _cache.Remove(Key);
                throw;
            } finally {
                // Set the event to allow waiting threads to continue.
                loadingPlaceHolder.Set();
            }
        }

        private void AddValueToCache(object value)
        {
            var placeholder = new ValuePlaceholder(value);
            _cache.Add(Key, placeholder, _expirationConfigs);
        }

        /// <summary>
        /// Indicates wether there is a value present or not.
        /// </summary>
        public bool HasValue
        {
            get { return GetValue() != null; }
        }

        /// <summary>
        /// Sets or gets the value in the current session.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// If you try to get a value while none is set.
        /// Use <see cref="ValueOrDefault"/> for safe access.
        /// </exception>
        public T Value
        {
            get
            {
                object v = GetOrLoadValue();

                if (v == null) {
                    throw new InvalidOperationException(String.Format("The cache does not contain any value for '{0}'.", Key));
                }

                return (T)v;
            }
        }

        public T ValueOrDefault
        {
            get
            {
                object v = GetOrLoadValue();

                return v == null 
                    ? default(T) 
                    : (T) v;
            }
        }

        /// <summary>
        /// Clears the value in the current session.
        /// </summary>
        public void Clear()
        {
            _cache.Remove(Key);
        }
    }
}