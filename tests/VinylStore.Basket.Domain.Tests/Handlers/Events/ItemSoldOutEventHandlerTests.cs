using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus.Testing;
using Shouldly;
using VinylStore.Basket.Domain.Handlers.Basket.Events;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStoreBasket.Fixtures;
using Xunit;

namespace VinylStore.Basket.Domain.Tests.Handlers.Events
{
    public class ItemSoldOutEventHandlerTests : IClassFixture<BasketContextFactory>
    {
        public ItemSoldOutEventHandlerTests(BasketContextFactory basketContextFactory)
        {
            _contextFactory = basketContextFactory;
            _itemSoldOutEventHandler = new ItemSoldOutEventHandler(_contextFactory.BasketRepository.Object);
        }

        private readonly ItemSoldOutEventHandler _itemSoldOutEventHandler;
        private readonly BasketContextFactory _contextFactory;

        [Fact]
        public async Task should_do_nothing_when_soldout_message_contains_not_existing_id()
        {
            var context = new TestableMessageHandlerContext();

            await _itemSoldOutEventHandler.Handle(
                new ItemSoldOutMessage {itemsId = new List<string> {Guid.NewGuid().ToString()}}, context);


            var basketsIds = _contextFactory.BasketRepository.Object.GetBaskets();

            var found = false;

            foreach (var basketId in basketsIds)
            {
                var basket = await _contextFactory.BasketRepository.Object.GetAsync(new Guid(basketId));
                found = basket.Items.Any(i => i.BasketItemId == new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"));
            }

            found.ShouldBeTrue();
        }


        [Fact]
        public async Task should_remove_correctly_when_soldout_messages_contains_existing_ids()
        {
            var context = new TestableMessageHandlerContext();

            await _itemSoldOutEventHandler.Handle(
                new ItemSoldOutMessage {itemsId = new List<string> {"be05537d-5e80-45c1-bd8c-aa21c0f1251e"}}, context);


            var basketsIds = _contextFactory.BasketRepository.Object.GetBaskets();

            var found = false;

            foreach (var basketId in basketsIds)
            {
                var basket = await _contextFactory.BasketRepository.Object.GetAsync(new Guid(basketId));
                found = basket.Items.Any(i => i.BasketItemId == new Guid("be05537d-5e80-45c1-bd8c-aa21c0f1251e"));
            }

            found.ShouldBeFalse();
        }
    }
}