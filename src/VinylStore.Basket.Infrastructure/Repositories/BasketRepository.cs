using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Infrastructure.Configurations;

namespace VinylStore.Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly BasketDataSourceSettings _settings;

        public BasketRepository(IOptions<BasketDataSourceSettings> options)
        {
            _settings = options.Value;
            var configuration = ConfigurationOptions.Parse(_settings.ConnectionString);
            configuration.AbortOnConnectFail = false;
            try
            {
                _database = ConnectionMultiplexer.Connect(configuration).GetDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public IEnumerable<string> GetBaskets()
        {
            var keys = _database.Multiplexer.GetServer(_settings.ConnectionString).Keys();
            return keys?.Select(k => k.ToString());
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
    }
}