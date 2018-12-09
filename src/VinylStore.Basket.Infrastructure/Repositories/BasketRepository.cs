using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using VinylStore.Basket.API.Infrastructure.Configurations;
using VinylStore.Basket.Domain.Infrastructure.Repositories;

namespace VinylStore.Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IOptions<BasketDataSourceSettings> options)
        {
            var configuration = ConfigurationOptions.Parse(options.Value.ConnectionString, true);
            configuration.ResolveDns = true;

            try
            {
                _database =  ConnectionMultiplexer.Connect(configuration).GetDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public async Task<Domain.Entities.Basket> GetAsync(Guid id)
        {
            var data = await _database.StringGetAsync(id.ToString());
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<Domain.Entities.Basket>(data);
        }

        public async Task<Domain.Entities.Basket> AddOrUpdateAsync(Domain.Entities.Basket item)
        {
            var created = await _database.StringSetAsync(item.Id, JsonConvert.SerializeObject(item));
            if (!created) return null;

            return await GetAsync(new Guid(item.Id));
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}