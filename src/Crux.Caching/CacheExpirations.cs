using System;
using System.Collections;
using System.Collections.Generic;

namespace Crux.Caching
{
    public class CacheExpirations : IEnumerable<CacheExpirationInfo>
    {
        private readonly IList<CacheExpirationInfo> _expirationConfigs;

        public CacheExpirations()
        {
            _expirationConfigs = new List<CacheExpirationInfo>();
        }

        public void AddAbsolute(TimeSpan timeFromNow)
        {
            AddExpiration("absolute", timeFromNow.ToString());
        }

        public void AddSliding(TimeSpan slidingTime)
        {
            AddExpiration("sliding", slidingTime.ToString());
        }

        public void AddFileDependency(string filePath)
        {
            AddExpiration("file", filePath);
        }

        private void AddExpiration(string type, string param)
        {
            _expirationConfigs.Add(new CacheExpirationInfo(type, param));
        }

        public IEnumerator<CacheExpirationInfo> GetEnumerator()
        {
            return _expirationConfigs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _expirationConfigs.GetEnumerator();
        }
    }
}