using System;

namespace VinylStore.Basket.Domain.Entities
{
    public class BasketItem
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; private set; }

        public int IncreaseQuantity()
        {
            return Quantity = Quantity++;
        }

        public int DecreaseQuantity()
        {
            return Quantity = Quantity--;
        }
    }
}