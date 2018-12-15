using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus.Testing;
using Shouldly;
using VinylStore.Cart.Domain.Handlers.Cart.Events;
using VinylStore.Cart.Domain.Requests.Cart;
using VinylStore.Cart.Fixtures;
using Xunit;

namespace VinylStore.Cart.Domain.Tests.Handlers.Events
{
    public class ItemSoldOutEventHandlerTests : IClassFixture<CartContextFactory>
    {
        public ItemSoldOutEventHandlerTests(CartContextFactory cartContextFactory)
        {
            _contextFactory = cartContextFactory;
            _itemSoldOutEventHandler = new ItemSoldOutEventHandler(_contextFactory.CartRepository.Object);
        }

        private readonly ItemSoldOutEventHandler _itemSoldOutEventHandler;
        private readonly CartContextFactory _contextFactory;

        [Fact]
        public async Task should_do_nothing_when_soldout_message_contains_not_existing_id()
        {
            var context = new TestableMessageHandlerContext();

            await _itemSoldOutEventHandler.Handle(
                new ItemSoldOutMessage {itemsId = new List<string> {Guid.NewGuid().ToString()}}, context);


            var cartsIds = _contextFactory.CartRepository.Object.GetCarts();

            var found = false;

            foreach (var cartId in cartsIds)
            {
                var cart = await _contextFactory.CartRepository.Object.GetAsync(new Guid(cartId));
                found = cart.Items.Any(i => i.CartItemId == new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"));
            }

            found.ShouldBeTrue();
        }


        [Fact]
        public async Task should_remove_correctly_when_soldout_messages_contains_existing_ids()
        {
            var context = new TestableMessageHandlerContext();

            await _itemSoldOutEventHandler.Handle(
                new ItemSoldOutMessage {itemsId = new List<string> {"be05537d-5e80-45c1-bd8c-aa21c0f1251e"}}, context);


            var cartsIds = _contextFactory.CartRepository.Object.GetCarts();

            var found = false;

            foreach (var cartId in cartsIds)
            {
                var cart = await _contextFactory.CartRepository.Object.GetAsync(new Guid(cartId));
                found = cart.Items.Any(i => i.CartItemId == new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"));
            }

            found.ShouldBeFalse();
        }
    }
}