using System;

namespace Crux.Caching
{
    public class CacheInitializer<T>
    {
        private readonly string _key;
        private readonly Func<T> _initializer;
        private ICacheInterceptor _interceptor;

        public CacheInitializer(string key, Func<T> initializer)
        {
            _key = key;
            _initializer = initializer;
            _interceptor = new DefaultCacheInterceptor();
        }

        public void SetInterceptor(ICacheInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        public T Initialize()
        {
            try {
                var value = _initializer.Invoke();
                _interceptor.OnSuccess(_key, value);

                return value;
            } catch (Exception e) {
                return (T)_interceptor.OnError(_key, e);
            }
        }
    }
}