using System;
using MediatR;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Requests.Basket
{
    public class UpdateBasketItemQuantityRequest : IRequest<BasketExtendedResponse>
    {
        public Guid BasketId { get; set; }

        public Guid BasketItemId { get; set; }

        public bool IsAddOperation { get; set; }

        public bool IsRemoveOperation => !IsAddOperation;
    }
}