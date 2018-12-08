using System;
using System.Threading.Tasks;

namespace VinylStore.Basket.Domain.Infrastructure.Repositories
{
    public interface IBasketRepository
    {
        Task<Entities.Basket> GetAsync(Guid id);
        Task<Entities.Basket> AddOrUpdateAsync(Entities.Basket item);
    }
}