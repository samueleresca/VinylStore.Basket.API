using AutoMapper;
using VinylStore.Basket.Domain.Entities;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Infrastructure.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItemResponse, BasketItem>().ReverseMap();
            CreateMap<BasketExtendedResponse, Entities.Basket>().ReverseMap();
            CreateMap<BasketUserResponse, BasketUser>().ReverseMap();
        }
    }
}