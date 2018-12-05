using System;
using System.Threading.Tasks;

namespace VinylStore.Basket.Domain.Infrastructure.Repositories
{
    public interface IBasketRepository
    {
        Task<Entities.Basket> GetAsync(Guid id);
        Task<Entities.Basket> AddAsync(Entities.Basket item);
        Task<Entities.Basket> UpdateAsync(Entities.Basket item);
    }
}