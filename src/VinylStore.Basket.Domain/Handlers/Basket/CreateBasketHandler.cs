using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Entities;
using VinylStore.Basket.Domain.Infrastructure.CatalogEnricher;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketRequest, BasketExtendedResponse>
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public CreateBasketHandler(IBasketRepository repository, IMapper mapper, ICatalogService catalogService)
        {
            _repository = repository;
            _mapper = mapper;
            _catalogService = catalogService;
        }

        public async Task<BasketExtendedResponse> Handle(
            CreateBasketRequest request,
            CancellationToken cancellationToken)
        {
            var entity = new Entities.Basket
            {
                Items = request.ItemsIds.Select(x => new BasketItem {BasketItemId = new Guid(x)}).ToList(),
                User = new BasketUser {Email = request.UserEmail},
                ValidityDate = DateTimeOffset.Now.AddMonths(2),
                Id = Guid.NewGuid().ToString()
            };

            var result = await _repository.AddOrUpdateAsync(entity);

            var response = _mapper.Map<BasketExtendedResponse>(result);

            var tasks = response.Items
                .Select(async x => await _catalogService.EnrichBasketItem(x, cancellationToken));

            response.Items = await Task.WhenAll(tasks);

            return response;
        }
    }
}