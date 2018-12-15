using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using VinylStore.Cart.Domain.Handlers.Cart;
using VinylStore.Cart.Domain.Infrastructure.Mapper;
using VinylStore.Cart.Domain.Requests.Cart;
using VinylStore.Cart.Fixtures;
using Xunit;

namespace VinylStore.Cart.Domain.Tests.Handlers
{
    public class UpdateCartItemQuantityTests : IClassFixture<CartContextFactory>
    {
        public UpdateCartItemQuantityTests(CartContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private readonly CartContextFactory _contextFactory;


        [Fact]
        public async Task handle_should_remove_items_with_quantity_0()
        {
            var handler = new UpdateCartItemQuantity(
                _contextFactory.CartRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CartProfile>())),
                _contextFactory.CatalogService.Object);

            var result = await handler.Handle(
                new UpdateCartItemQuantityRequest
                {
                    CartId = new Guid("9ced6bfa-9659-462e-aece-49fe50613e96"),
                    CartItemId = new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"),
                    IsAddOperation = false
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task handle_should_retrieve_item_with_increase_quantity()
        {
            var handler = new UpdateCartItemQuantity(
                _contextFactory.CartRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CartProfile>())),
                _contextFactory.CatalogService.Object);

            var result = await handler.Handle(
                new UpdateCartItemQuantityRequest
                {
                    CartId = new Guid("9ced6bfa-9659-462e-aece-49fe50613e96"),
                    CartItemId = new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"),
                    IsAddOperation = true
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.First(x => x.CartItemId == "be05537d-5e80-45c1-bd8c-aa21c0f1251e").Quantity.ShouldBe(2);
            result.User.ShouldNotBeNull();
            result.ValidityDate.ShouldNotBeNull();
        }
    }
}