using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class UpdateBasketItemQuantity : IRequestHandler<UpdateBasketItemQuantityRequest, BasketExtendedResponse>
    {
        public Task<BasketExtendedResponse> Handle(UpdateBasketItemQuantityRequest request,
            CancellationToken cancellationToken)
        {
        }
    }
}