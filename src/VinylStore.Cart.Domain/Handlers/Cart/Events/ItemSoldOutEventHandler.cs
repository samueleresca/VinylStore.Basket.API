using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using VinylStore.Cart.Domain.Infrastructure.Repositories;
using VinylStore.Cart.Domain.Requests.Cart;

namespace VinylStore.Cart.Domain.Handlers.Cart.Events
{
    public class ItemSoldOutEventHandler : IHandleMessages<ItemSoldOutMessage>
    {
        private readonly ICartRepository _cartRepository;

        public ItemSoldOutEventHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(ItemSoldOutMessage message, IMessageHandlerContext context)
        {
            var cartIds = _cartRepository.GetCarts().ToList();

            var tasks = cartIds.Select(async x =>
            {
                var cart = await _cartRepository.GetAsync(new Guid(x));
                await RemoveItemsInCart(message.itemsId, cart);
            });

            await Task.WhenAll(tasks);
        }

        private async Task RemoveItemsInCart(List<string> eventItemsId, Entities.Cart cart)
        {
            if (eventItemsId == null || eventItemsId.Count == 0) return;

            var toDelete = cart?.Items?.Where(x => eventItemsId.Contains(x.CartItemId.ToString())).ToList();

            if (toDelete == null || toDelete.Count == 0) return;

            foreach (var item in toDelete) cart.Items?.Remove(item);

            await _cartRepository.AddOrUpdateAsync(cart);
        }
    }
}