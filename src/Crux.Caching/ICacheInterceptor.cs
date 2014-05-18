using System;

namespace Crux.Caching
{
    public interface ICacheInterceptor
    {
        void OnSuccess(string key, object value);
        object OnError(string key, Exception e);
    }
}