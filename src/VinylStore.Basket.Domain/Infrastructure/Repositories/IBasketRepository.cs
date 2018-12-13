using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VinylStore.Basket.Domain.Infrastructure.Repositories
{
    public interface IBasketRepository
    {
        IEnumerable<string> GetBaskets();
        Task<Entities.Basket> GetAsync(Guid id);
        Task<Entities.Basket> AddOrUpdateAsync(Entities.Basket item);
    }
}