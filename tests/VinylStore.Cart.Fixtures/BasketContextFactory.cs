using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using VinylStore.Cart.Domain.Infrastructure.Repositories;
using VinylStore.Cart.Domain.Infrastructure.Services;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Fixtures
{
    public class CartContextFactory
    {
        public readonly Mock<ICartRepository> CartRepository;
        public readonly Mock<ICatalogService> CatalogService;

        public CartContextFactory()
        {
            Dictionary<Guid, CartItemResponse> itemResponses;
            Dictionary<Guid, string> memoryCollection;

            using (var reader = new StreamReader("./Data/cart.json"))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Domain.Entities.Cart[]>(json);

                memoryCollection =
                    data.ToDictionary(cart => new Guid(cart.Id), JsonConvert.SerializeObject);
            }

            using (var reader = new StreamReader("./Data/items.json"))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<CartItemResponse[]>(json);

                itemResponses = data.ToDictionary(item => new Guid(item.CartItemId), item => item);
            }


            CartRepository = new Mock<ICartRepository>();
            CatalogService = new Mock<ICatalogService>();


            CartRepository
                .Setup(x => x.GetCarts())
                .Returns(() => memoryCollection.Keys.Select(x => x.ToString()).ToList());


            CartRepository
                .Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                    Task.FromResult(JsonConvert.DeserializeObject<Domain.Entities.Cart>(memoryCollection[id])));

            CartRepository
                .Setup(x => x.AddOrUpdateAsync(It.IsAny<Domain.Entities.Cart>()))
                .Callback((Domain.Entities.Cart x) =>
                {
                    memoryCollection[new Guid(x.Id)] = JsonConvert.SerializeObject(x);
                })
                .ReturnsAsync((Domain.Entities.Cart x) =>
                    JsonConvert.DeserializeObject<Domain.Entities.Cart>(memoryCollection[new Guid(x.Id)]));

            CatalogService
                .Setup(x => x.EnrichCartItem(It.IsAny<CartItemResponse>(), CancellationToken.None))
                .ReturnsAsync((CartItemResponse item, CancellationToken token) =>
                {
                    item.Description = itemResponses[new Guid(item.CartItemId)].Description;
                    item.Name = itemResponses[new Guid(item.CartItemId)].Name;
                    item.Price = itemResponses[new Guid(item.CartItemId)].Price;
                    item.ArtistName = itemResponses[new Guid(item.CartItemId)].ArtistName;
                    item.GenreDescription = itemResponses[new Guid(item.CartItemId)].GenreDescription;
                    item.PictureUri = itemResponses[new Guid(item.CartItemId)].PictureUri;
                    return item;
                });
        }
    }
}