using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket.Events
{
    public class ItemSoldOutEventHandler : IHandleMessages<ItemSoldOutMessage>
    {
        private readonly IBasketRepository _basketRepository;

        public ItemSoldOutEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(ItemSoldOutMessage message, IMessageHandlerContext context)
        {
            var basketIds = _basketRepository.GetBaskets().ToList();

            foreach (var id in basketIds)
            {
                var basket = await _basketRepository.GetAsync(new Guid(id));
                await RemoveItemsInBasket(message.itemsId, basket);
            }
        }

        private async Task RemoveItemsInBasket(List<string> eventItemsId, Entities.Basket basket)
        {
            if (eventItemsId == null || eventItemsId.Count == 0) return;

            var toDelete = basket?.Items?.Where(x => eventItemsId.Contains(x.BasketItemId.ToString())).ToList();

            if (toDelete == null || toDelete.Count == 0) return;

            foreach (var item in toDelete) basket?.Items?.Remove(item);

            await _basketRepository.AddOrUpdateAsync(basket);
        }
    }
}