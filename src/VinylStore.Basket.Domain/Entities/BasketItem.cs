using System;

namespace VinylStore.Basket.Domain.Entities
{
    public class BasketItem
    {
        public Guid BasketItemId { get; set; }

        public int Quantity { get; set; }

        public void IncreaseQuantity()
        {
            Quantity = Quantity + 1;
        }

        public void DecreaseQuantity()
        {
            Quantity = Quantity - 1;
        }
    }
}