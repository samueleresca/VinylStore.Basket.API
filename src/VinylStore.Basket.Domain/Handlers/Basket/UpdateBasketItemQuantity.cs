using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Infrastructure.CatalogEnricher;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class UpdateBasketItemQuantity : IRequestHandler<UpdateBasketItemQuantityRequest, BasketExtendedResponse>
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public UpdateBasketItemQuantity(IBasketRepository repository, IMapper mapper, ICatalogService catalogService)
        {
            _repository = repository;
            _mapper = mapper;
            _catalogService = catalogService;
        }

        public async Task<BasketExtendedResponse> Handle(UpdateBasketItemQuantityRequest request,
            CancellationToken cancellationToken)
        {
            var basketDetail = await _repository.GetAsync(request.BasketId);

            if (request.IsAddOperation)
                basketDetail.Items.FirstOrDefault(_ => _.BasketItemId == request.BasketItemId)?.IncreaseQuantity();
            else
                basketDetail.Items.FirstOrDefault(_ => _.BasketItemId == request.BasketItemId)?.DecreaseQuantity();

            var basketItemsList = basketDetail.Items.ToList();
            basketItemsList.RemoveAll(x => x.Quantity <= 0);

            basketDetail.Items = basketItemsList;

            var response = _mapper.Map<BasketExtendedResponse>(basketDetail);

            var tasks = response.Items
                .Select(async x => await _catalogService.EnrichBasketItem(x, cancellationToken));

            response.Items = await Task.WhenAll(tasks);

            return response;
        }
    }
}