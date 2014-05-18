using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Crux.Caching.Tests
{
    public class MockCache : ICache
    {
        private readonly ConcurrentDictionary<string, object> _backingStore;

        public MockCache()
        {
            _backingStore = new ConcurrentDictionary<string, object>();
        }

        public object Get(string key)
        {
            object value;
            _backingStore.TryGetValue(key, out value);
            return value;
        }

        public void Add(string key, object value)
        {
            if (_backingStore.ContainsKey(key)) {
                object removed;
                _backingStore.TryRemove(key, out removed);
            }

            _backingStore.TryAdd(key, value);
        }

        public void Add(string key, object value, IEnumerable<CacheExpirationInfo> expirations)
        {
            Add(key, value);
        }

        public void Remove(string key)
        {
            object value;
            _backingStore.TryRemove(key, out value);
        }
    }
}
