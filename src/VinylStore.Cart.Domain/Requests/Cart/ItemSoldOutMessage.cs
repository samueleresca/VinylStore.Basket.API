using System.Collections.Generic;
using NServiceBus;

namespace VinylStore.Cart.Domain.Requests.Cart
{
    public class ItemSoldOutMessage : IMessage
    {
        public List<string> itemsId { get; set; }
    }
}