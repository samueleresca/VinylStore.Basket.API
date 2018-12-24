using System;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus.Testing;
using Shouldly;
using VinylStore.Cart.Domain.Handlers.Cart.Events;
using VinylStore.Cart.Events;
using VinylStore.Cart.Fixtures;
using Xunit;

namespace VinylStore.Cart.Domain.Tests.Handlers.Events
{
    public class ItemSoldOutEventHandlerTests : IClassFixture<CartContextFactory>
    {
        public ItemSoldOutEventHandlerTests(CartContextFactory cartContextFactory)
        {
            _contextFactory = cartContextFactory;
        }

        private readonly CartContextFactory _contextFactory;

        [Fact]
        public async Task should_not_remove_records_when_soldout_message_contains_not_existing_id()
        {
            var context = new TestableMessageHandlerContext();
            var repository = _contextFactory.GetCartRepository();
            var itemSoldOutEventHandler = new ItemSoldOutEventHandler(repository);
            var found = false;

            await itemSoldOutEventHandler.Handle(
                new ItemSoldOutEvent {Id = Guid.NewGuid().ToString()}, context);

            var cartsIds = repository.GetCarts();

            foreach (var cartId in cartsIds)
            {
                var cart = await repository.GetAsync(cartId);
                found = cart.Items.Any(i => i.CartItemId == "be05537d-5e80-45c1-bd8c-aa21c0f1251e");
            }

            found.ShouldBeTrue();
        }


        [Fact]
        public async Task should_remove_records_when_soldout_messages_contains_existing_ids()
        {
            var context = new TestableMessageHandlerContext();
            var repository = _contextFactory.GetCartRepository();
            var itemSoldOutEventHandler = new ItemSoldOutEventHandler(repository);
            var found = false;


            await itemSoldOutEventHandler.Handle(
                new ItemSoldOutEvent { Id = "be05537d-5e80-45c1-bd8c-aa21c0f1251e"}, context);

            foreach (var cartId in repository.GetCarts())
            {
                var cart = await repository.GetAsync(cartId);
                found = cart.Items.Any(i => i.CartItemId == "be05537d-5e80-45c1-bd8c-aa21c0f1251e");
            }

            found.ShouldBeFalse();
        }
    }
}