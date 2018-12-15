using System.Collections.Generic;
using MediatR;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Domain.Requests.Cart
{
    public class CreateCartRequest : IRequest<CartExtendedResponse>
    {
        public IList<string> ItemsIds { get; set; }

        public string UserEmail { get; set; }
    }
}