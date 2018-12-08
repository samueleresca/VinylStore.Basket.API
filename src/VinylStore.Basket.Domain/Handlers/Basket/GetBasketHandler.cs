using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;
using VinylStore.Catalog.API.Client;
using VinylStore.Catalog.Contract.Item;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class GetBasketHandler : IRequestHandler<GetBasketRequest, BasketExtendedResponse>
    {
        private readonly ICatalogClient _catalogClient;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;


        public GetBasketHandler(IBasketRepository repository, IMapper mapper, ICatalogClient catalogClient)
        {
            _repository = repository;
            _mapper = mapper;
            _catalogClient = catalogClient;
        }

        public async Task<BasketExtendedResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(request.Id);

            var extendedResponse = _mapper.Map<BasketExtendedResponse>(result);
            extendedResponse.Items = extendedResponse.Items.Select(_ => EnrichBasketItem(_, cancellationToken));

            return extendedResponse;
        }

        private async Task<BasketItemResponse> EnrichBasketItem(BasketItemResponse item,
            CancellationToken cancellationToken)
        {
            var result = await _catalogClient.Item.Get(item.BasketItemId, cancellationToken);
            return Map(item, result);
        }

        private static BasketItemResponse Map(BasketItemResponse item, ItemResponse result)
        {
            item.Description = result.Description;
            item.Name = result.Name;
            item.Price = result.Price.Amount;
            item.ArtistName = result.Artist.ArtistName;
            item.GenreDescription = result.Genre.GenreDescription;
            item.PictureUri = result.PictureUri;

            return item;
        }
    }
}