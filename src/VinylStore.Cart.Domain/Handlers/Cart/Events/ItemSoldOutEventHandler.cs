using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using VinylStore.Cart.Domain.Infrastructure.Repositories;
using VinylStore.Cart.Events;

namespace VinylStore.Cart.Domain.Handlers.Cart.Events
{
    public class ItemSoldOutEventHandler : IHandleMessages<ItemSoldOutEvent>
    {
        private readonly ICartRepository _cartRepository;

        public ItemSoldOutEventHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(ItemSoldOutEvent @event, IMessageHandlerContext context)
        {
            var cartIds = _cartRepository.GetCarts().ToList();

            var tasks = cartIds.Select(async x =>
            {
                var cart = await _cartRepository.GetAsync(x);
                await RemoveItemsInCart(@event.Id, cart);
            });

            await Task.WhenAll(tasks);
        }

        private async Task RemoveItemsInCart(string itemToRemove, Entities.Cart cart)
        {
            if (string.IsNullOrEmpty(itemToRemove)) return;

            var toDelete = cart?.Items?.Where(x => x.CartItemId == itemToRemove).ToList();

            if (toDelete == null || toDelete.Count == 0) return;

            foreach (var item in toDelete) cart.Items?.Remove(item);

            await _cartRepository.AddOrUpdateAsync(cart);
        }
    }
}