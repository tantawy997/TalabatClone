using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Serivces
{
    public class ServiceCache : IResponseCache
    {

        public readonly IDatabase redis; 
        public ServiceCache(IConnectionMultiplexer Redis) 
        {
            redis = Redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var serliziedResponse = JsonSerializer.Serialize(response, options);

            await redis.StringSetAsync(cacheKey,serliziedResponse,timeToLive);

        }

        public async Task<string> GetCacheResponse(string cacheKey)
        {
            var cachedResponse = await redis.StringGetAsync(cacheKey);
            if (cachedResponse.IsNullOrEmpty)
                return null;

            return cachedResponse.ToString();

        }
    }
}
