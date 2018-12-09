using System;
using System.Collections.Generic;

namespace VinylStore.Basket.Domain.Entities
{
    public class Basket
    {
        public string Id { get; set; }

        public IList<BasketItem> Items { get; set; }

        public BasketUser User { get; set; }

        public DateTimeOffset ValidityDate { get; set; }
    }
}