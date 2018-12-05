using System;

namespace VinylStore.Basket.Domain.Entities
{
    public class BasketItem
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}