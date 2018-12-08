using System.Collections.Generic;
using MediatR;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Requests.Basket
{
    public class CreateBasketRequest : IRequest<BasketExtendedResponse>
    {
        public IList<string> ItemsIds { get; set; }

        public string UserEmail { get; set; }

        public string ValidityDate { get; set; }
    }
}