using System.Threading;
using System.Threading.Tasks;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Domain.Infrastructure.Services
{
    public interface ICatalogService
    {
        Task<CartItemResponse> EnrichCartItem(
            CartItemResponse item,
            CancellationToken cancellationToken);
    }
}