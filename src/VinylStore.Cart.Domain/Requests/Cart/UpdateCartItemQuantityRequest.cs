using System;
using MediatR;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Domain.Requests.Cart
{
    public class UpdateCartItemQuantityRequest : IRequest<CartExtendedResponse>
    {
        public Guid CartId { get; set; }

        public Guid CartItemId { get; set; }

        public bool IsAddOperation { get; set; }

        public bool IsRemoveOperation => !IsAddOperation;
    }
}