using System;
using MediatR;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Requests.Basket
{
    public class GetBasketRequest : IRequest<BasketExtendedResponse>
    {
        public Guid Id { get; set; }
    }
}