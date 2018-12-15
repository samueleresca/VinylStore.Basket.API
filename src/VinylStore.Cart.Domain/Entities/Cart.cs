using System;
using System.Collections.Generic;

namespace VinylStore.Cart.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; }

        public IList<CartItem> Items { get; set; }

        public CartUser User { get; set; }

        public DateTimeOffset ValidityDate { get; set; }
    }
}