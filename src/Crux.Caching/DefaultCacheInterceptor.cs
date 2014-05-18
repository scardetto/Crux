using System;

namespace Crux.Caching
{
    public class DefaultCacheInterceptor : ICacheInterceptor
    {
        public void OnSuccess(string key, object value)
        {
            // Do nothing
        }

        public object OnError(string key, Exception e)
        {
            throw e;
        }
    }
}