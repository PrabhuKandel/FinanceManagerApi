

using FinanceManager.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FinanceManager.Infrastructure.Caching
{
    public class RedisCacheService(IDistributedCache _distributedCache) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key)
        {
            var cached = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cached))
                return default; // null for reference types

            return JsonConvert.DeserializeObject<T>(cached);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            var serialized = JsonConvert.SerializeObject(value);
            var options = ttl.HasValue
                ? new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = ttl.Value }
                :  new DistributedCacheEntryOptions();

            await _distributedCache.SetStringAsync(key, serialized, options);
        }
    }
}
