using Core.entites;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase redis;

        public BasketRepo(IConnectionMultiplexer _Redis)
        {
            redis = _Redis.GetDatabase();
        }
        public async Task DeleteBasketAsync(string id)
        {
            await redis.KeyDeleteAsync(id);
         }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var data = await redis.StringGetAsync(id);

            return data.IsNullOrEmpty?null: JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var data = await redis.StringSetAsync(basket.Id,
                JsonSerializer.Serialize<CustomerBasket>(basket), TimeSpan.FromMinutes(10));

            if (!data)
                return null;

            
            return await GetBasketAsync(basket.Id);
          
        }
    }
}
