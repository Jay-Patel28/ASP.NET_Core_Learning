using AutoMapper.Execution;
using Cinema.Entities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Cinema.Caching
{
    public class InMemoryCache : ICaching
    {
        private readonly IMemoryCache memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public string Get(string Key)
        {
            memoryCache.TryGetValue(Key, out var value);
            if (value == null) return null;
            return JsonConvert.SerializeObject(value);
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }
        public void Set(string key, object data)
        {
            if (data != null)
            {
                memoryCache.Set(key, data);
            }
        }
    }

}
