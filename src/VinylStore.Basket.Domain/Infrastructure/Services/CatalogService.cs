using System;
using System.Threading;
using System.Threading.Tasks;
using VinylStore.Basket.Domain.Responses.Basket;
using VinylStore.Catalog.API.Client;
using VinylStore.Catalog.Contract.Item;

namespace VinylStore.Basket.Domain.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogClient _catalogClient;

        public CatalogService(ICatalogClient catalogClient)
        {
            _catalogClient = catalogClient;
        }

        public async Task<BasketItemResponse> EnrichBasketItem(
            BasketItemResponse item,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _catalogClient.Item.Get(new Guid(item.BasketItemId), cancellationToken);
                return Map(item, result);
            }
            catch (Exception e)
            {
                return item;
            }
        }

        private static BasketItemResponse Map(BasketItemResponse item, ItemResponse result)
        {
            item.Description = result.Description;
            item.Name = result.Name;
            item.Price = result.Price.Amount.ToString();
            item.ArtistName = result.Artist.ArtistName;
            item.GenreDescription = result.Genre.GenreDescription;
            item.PictureUri = result.PictureUri;

            return item;
        }
    }
}