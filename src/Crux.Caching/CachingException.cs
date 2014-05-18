using System;

namespace Crux.Caching
{
    /// <summary>
    /// Summary description for CacheLoadingException.
    /// </summary>
    public class CachingException : ApplicationException
    {
        public CachingException() { }

        public CachingException(string s) : base(s) { }

        public CachingException(string s, Exception e) : base(s, e) { }
    }
}