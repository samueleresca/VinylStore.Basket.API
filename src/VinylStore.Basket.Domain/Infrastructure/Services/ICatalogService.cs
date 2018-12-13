using System.Threading;
using System.Threading.Tasks;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Infrastructure.Services
{
    public interface ICatalogService
    {
        Task<BasketItemResponse> EnrichBasketItem(
            BasketItemResponse item,
            CancellationToken cancellationToken);
    }
}