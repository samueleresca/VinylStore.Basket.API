using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;
using VinylStore.Basket.Domain.Infrastructure.Repositories;

namespace VinylStore.Basket.Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Domain.Entities.Basket> GetAsync(Guid id)
        {
            var data = await _database.StringGetAsync(id.ToString());
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<Domain.Entities.Basket>(data);
        }

        public async Task<Domain.Entities.Basket> AddOrUpdateAsync(Domain.Entities.Basket item)
        {
            var created = await _database.StringSetAsync(item.Id.ToString(), JsonConvert.SerializeObject(item));
            if (!created) return null;

            return await GetAsync(item.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}