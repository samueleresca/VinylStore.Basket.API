using System.Collections.Generic;
using NServiceBus;

namespace VinylStore.Basket.Domain.Requests.Basket
{
    public class ItemSoldOutMessage : IMessage
    {
        public List<string> itemsId { get; set; }
    }
}