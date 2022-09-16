using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using Newtonsoft;
using Cinema.Models;
using System.Collections.Generic;
using System.Collections;

namespace Cinema.Caching
{
    public class RedisCache : ICaching
    {
        private readonly IDistributedCache distributedCache;
        public RedisCache(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public string Get(string Key)
        {
            byte[] bytes = distributedCache.Get(Key);
            if (bytes == null)
            {
                return null;
            }
            //object val = JsonSerializer.Deserialize < List < ActorModel>> ((Encoding.UTF8.GetString(bytes)));
            //Console.WriteLine(val);
            //object val = JsonSerializer.SerializeToUtf8Bytes(bytes);
            return Encoding.UTF8.GetString(bytes);
        }

        public void Remove(string key)
        {
            distributedCache.Remove(key);
        }

        public void Set(string key, object data)
        {
            distributedCache.Set(key, JsonSerializer.SerializeToUtf8Bytes(data));
        }
    }
}
