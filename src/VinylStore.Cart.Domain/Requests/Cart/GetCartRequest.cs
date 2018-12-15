using System;
using MediatR;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Domain.Requests.Cart
{
    public class GetCartRequest : IRequest<CartExtendedResponse>
    {
        public Guid Id { get; set; }
    }
}