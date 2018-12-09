using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using VinylStore.Basket.Domain.Handlers.Basket;
using VinylStore.Basket.Domain.Infrastructure.Mapper;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStoreBasket.Fixtures;
using Xunit;

namespace VinylStore.Basket.Domain.Tests.Handlers
{
    public class UpdateBasketItemQuantityTests : IClassFixture<BasketContextFactory>
    {
        public UpdateBasketItemQuantityTests(BasketContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private readonly BasketContextFactory _contextFactory;


        [Fact]
        public async Task handle_should_remove_items_with_quantity_0()
        {
            var handler = new UpdateBasketItemQuantity(
                _contextFactory.BasketRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<BasketProfile>())),
                _contextFactory.CatalogService.Object);

            var result = await handler.Handle(
                new UpdateBasketItemQuantityRequest
                {
                    BasketId = new Guid("9ced6bfa-9659-462e-aece-49fe50613e96"),
                    BasketItemId = new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"),
                    IsAddOperation = false
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task handle_should_retrieve_item_with_increase_quantity()
        {
            var handler = new UpdateBasketItemQuantity(
                _contextFactory.BasketRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<BasketProfile>())),
                _contextFactory.CatalogService.Object);

            var result = await handler.Handle(
                new UpdateBasketItemQuantityRequest
                {
                    BasketId = new Guid("9ced6bfa-9659-462e-aece-49fe50613e96"),
                    BasketItemId = new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"),
                    IsAddOperation = true
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.First(x => x.BasketItemId == "be05537d-5e80-45c1-bd8c-aa21c0f1251e").Quantity.ShouldBe(2);
            result.User.ShouldNotBeNull();
            result.ValidityDate.ShouldNotBeNull();
        }
    }
}