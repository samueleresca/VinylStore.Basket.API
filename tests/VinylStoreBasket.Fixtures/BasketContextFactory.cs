using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using VinylStore.Basket.Domain.Entities;
using VinylStore.Basket.Domain.Infrastructure.CatalogEnricher;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStoreBasket.Fixtures
{
    public class BasketContextFactory
    {
        private readonly Dictionary<Guid, BasketItemResponse> _itemResponses;
        private readonly Dictionary<Guid, string> _memoryCollection;

        public readonly Mock<IBasketRepository> BasketRepository;
        public readonly Mock<ICatalogService> CatalogService;

        public BasketContextFactory()
        {
            _memoryCollection = new Dictionary<Guid, string>();

            using (var reader = new StreamReader("./Data/basket.json"))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Basket[]>(json);

                _memoryCollection =
                    data.ToDictionary(basket => new Guid(basket.Id), JsonConvert.SerializeObject);
            }

            using (var reader = new StreamReader("./Data/items.json"))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<BasketItemResponse[]>(json);

                _itemResponses = data.ToDictionary(item => new Guid(item.BasketItemId), item => item);
            }


            BasketRepository = new Mock<IBasketRepository>();
            CatalogService = new Mock<ICatalogService>();

            BasketRepository
                .Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                    Task.FromResult(JsonConvert.DeserializeObject<Basket>(_memoryCollection[id])));

            BasketRepository
                .Setup(x => x.AddOrUpdateAsync(It.IsAny<Basket>()))
                .Callback((Basket x) => { _memoryCollection[new Guid(x.Id)] = JsonConvert.SerializeObject(x); })
                .ReturnsAsync((Basket x) =>
                    JsonConvert.DeserializeObject<Basket>(_memoryCollection[new Guid(x.Id)]));

            CatalogService
                .Setup(x => x.EnrichBasketItem(It.IsAny<BasketItemResponse>(), CancellationToken.None))
                .ReturnsAsync((BasketItemResponse item, CancellationToken token) =>
                {
                    item.Description = _itemResponses[new Guid(item.BasketItemId)].Description;
                    item.Name = _itemResponses[new Guid(item.BasketItemId)].Name;
                    item.Price = _itemResponses[new Guid(item.BasketItemId)].Price;
                    item.ArtistName = _itemResponses[new Guid(item.BasketItemId)].ArtistName;
                    item.GenreDescription = _itemResponses[new Guid(item.BasketItemId)].GenreDescription;
                    item.PictureUri = _itemResponses[new Guid(item.BasketItemId)].PictureUri;
                    return item;
                });
        }
    }
}