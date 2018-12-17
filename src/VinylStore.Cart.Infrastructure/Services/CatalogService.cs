using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VinylStore.Cart.Domain.Infrastructure.Services;
using VinylStore.Cart.Domain.Responses.Cart;
using VinylStore.Catalog.API.Client;
using VinylStore.Catalog.Contract.Item;

namespace VinylStore.Cart.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogClient _catalogClient;

        public CatalogService(ICatalogClient catalogClient)
        {
            _catalogClient = catalogClient;
        }

        public async Task<CartItemResponse> EnrichCartItem(
            CartItemResponse item,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _catalogClient.Item.Get(new Guid(item.CartItemId), cancellationToken);
                return Map(item, result);
            }
            catch (Exception e)
            {
                return item;
            }
        }

        private static CartItemResponse Map(CartItemResponse item, ItemResponse result)
        {
            item.Description = result.Description;
            item.LabelName = result.LabelName;
            item.Name = result.Name;
            item.Price = result.Price.Amount.ToString(CultureInfo.InvariantCulture);
            item.ArtistName = result.Artist.ArtistName;
            item.GenreDescription = result.Genre.GenreDescription;
            item.PictureUri = result.PictureUri;

            return item;
        }
    }
}