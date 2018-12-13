using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Infrastructure.Services;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class GetBasketHandler : IRequestHandler<GetBasketRequest, BasketExtendedResponse>
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public GetBasketHandler(
            IBasketRepository repository,
            IMapper mapper,
            ICatalogService catalogService)
        {
            _repository = repository;
            _mapper = mapper;
            _catalogService = catalogService;
        }

        public async Task<BasketExtendedResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(request.Id);
            var extendedResponse = _mapper.Map<BasketExtendedResponse>(result);

            var tasks = extendedResponse.Items
                .Select(async x => await _catalogService.EnrichBasketItem(x, cancellationToken));

            extendedResponse.Items = await Task.WhenAll(tasks);
            return extendedResponse;
        }
    }
}