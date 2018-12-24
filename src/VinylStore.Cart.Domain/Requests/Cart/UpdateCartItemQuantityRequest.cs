using System;
using MediatR;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Domain.Requests.Cart
{
    public class UpdateCartItemQuantityRequest : IRequest<CartExtendedResponse>
    {
        public string CartId { get; set; }

        public string CartItemId { get; set; }

        public bool IsAddOperation { get; set; }

        public bool IsRemoveOperation => !IsAddOperation;
    }
}