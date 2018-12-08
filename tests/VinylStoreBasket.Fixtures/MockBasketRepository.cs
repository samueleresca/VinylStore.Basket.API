using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VinylStore.Basket.Domain.Entities;
using VinylStore.Basket.Domain.Infrastructure.Repositories;

namespace VinylStoreBasket.Fixtures
{
    public class MockBasketRepository : IBasketRepository
    {
        private readonly Dictionary<Guid, string> _collection;

        public MockBasketRepository()
        {
            _collection = new Dictionary<Guid, string>();
        }

        public Task<Basket> GetAsync(Guid id)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<Basket>(_collection[id]));
        }

        public Task<Basket> AddOrUpdateAsync(Basket item)
        {
            _collection[item.Id] = JsonConvert.SerializeObject(item);
            return Task.FromResult(item);
        }
    }
}