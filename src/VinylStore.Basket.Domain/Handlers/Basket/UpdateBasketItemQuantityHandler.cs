using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class UpdateBasketItemQuantity : IRequestHandler<UpdateBasketItemQuantityRequest, BasketExtendedResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public UpdateBasketItemQuantity(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BasketExtendedResponse> Handle(UpdateBasketItemQuantityRequest request,
            CancellationToken cancellationToken)
        {
            var basketDetail = await _repository.GetAsync(request.BasketId);
            var basketItems = basketDetail.Items.First(_ => _.BasketItemId == request.BasketItemId);
            if (request.IsAddOperation)
                basketItems.IncreaseQuantity();
            else
                basketItems.DecreaseQuantity();

            await _repository.AddOrUpdateAsync(basketDetail);
            return _mapper.Map<BasketExtendedResponse>(basketDetail);
        }
    }
}