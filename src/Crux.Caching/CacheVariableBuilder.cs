using System;
using System.Collections.Generic;
using System.Threading;

namespace Crux.Caching
{
    public class CacheVariableBuilder<T>
    {
        private readonly ICache _cache;

        private string _key;
        private Func<T> _initializer;
        private int _initializationTimeout;
        private ICacheInterceptor _interceptor;
        private bool _lazyLoad;
        private readonly IList<CacheExpirationInfo> _expirationConfigs;

        public CacheVariableBuilder(ICache cache)
        {
            _cache = cache;
            _expirationConfigs = new List<CacheExpirationInfo>();
            _initializationTimeout = Timeout.Infinite;
            _lazyLoad = true;
        }

        public CacheVariableBuilder<T> WithKey(string key)
        {
            _key = key;
            return this;
        }

        public CacheVariableBuilder<T> InitializeWith(Func<T> initializer)
        {
            _initializer = initializer;
            return this;
        }

        public CacheVariableBuilder<T> TimeoutAfter(TimeSpan timespan)
        {
            _initializationTimeout = Convert.ToInt32(timespan.TotalMilliseconds);
            return this;
        }

        public CacheVariableBuilder<T> EagerFetch()
        {
            _lazyLoad = false;
            return this;
        }

        public CacheVariableBuilder<T> LazyLoad()
        {
            _lazyLoad = true;
            return this;
        }

        public CacheVariableBuilder<T> SaveBackupToFile(ICacheFilePathBuilder pathBuilder)
        {
            return WrapInitializationWith(new BackupFileInterceptor(pathBuilder));
        } 

        public CacheVariableBuilder<T> WrapInitializationWith(ICacheInterceptor interceptor)
        {
            _interceptor = interceptor;
            return this;
        }

        public CacheVariableBuilder<T> AddAbsoluteExpiration(TimeSpan timeFromNow)
        {
            AddExpiration("absolute", timeFromNow.ToString());
            return this;
        }

        public CacheVariableBuilder<T> AddSlidingExpiration(TimeSpan slidingTime)
        {
            AddExpiration("sliding", slidingTime.ToString());
            return this;
        }

        public CacheVariableBuilder<T> AddExtendedExpiration(string timeFormat)
        {
            AddExpiration("extended", timeFormat);
            return this;
        }

        public CacheVariableBuilder<T> AddFileDependency(string filePath)
        {
            AddExpiration("file", filePath);
            return this;
        }

        private void AddExpiration(string type, string param)
        {
            _expirationConfigs.Add(new CacheExpirationInfo(type, param));
        }

        public CacheVariableBuilder<T> SetNeverExpires()
        {
            if (_expirationConfigs.Count > 0) {
                _expirationConfigs.Clear();
            }

            _expirationConfigs.Add(new CacheExpirationInfo("never", null));
            return this;
        }


        public CacheVariable<T> Create()
        {
            AssertValid();

            return new CacheVariable<T>(
                _cache, 
                _key, 
                GetInitializer(), 
                _expirationConfigs, 
                _initializationTimeout, 
                _lazyLoad);
        } 

        private CacheInitializer<T> GetInitializer()
        {
            var initializer = new CacheInitializer<T>(_key, _initializer);

            if (_interceptor != null) {
                initializer.SetInterceptor(_interceptor);
            }

            return initializer;
        } 

        private void AssertValid()
        {
            if (String.IsNullOrEmpty(_key)) {
                throw new CachingException("Cache key not specified. Make sure you make a call to 'WithKey(string)'");
            }

            if (_initializer == null) {
                throw new CachingException("Initializer not specified. Make sure you make a call to 'InitializeWith(Func<T>)'");
            }
        }
    }
}