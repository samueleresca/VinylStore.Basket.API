using System;
using System.Collections.Generic;
using VinylStore.Basket.Domain.Entities;

namespace VinylStore.Basket.Domain.Responses.Basket
{
    public class BasketExtendedResponse
    {
        public Guid Id { get; set; }

        public IList<BasketItemResponse> Items { get; set; }

        public BasketUser User { get; set; }

        public DateTimeOffset ValidityDate { get; set; }
    }
}