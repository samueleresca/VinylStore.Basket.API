using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using VinylStore.Basket.Domain.Handlers.Basket;
using VinylStore.Basket.Domain.Infrastructure.Mapper;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStoreBasket.Fixtures;
using Xunit;

namespace VinylStore.Basket.Domain.Tests.Handlers
{
    public class GetBasketHandlerTests : IClassFixture<BasketContextFactory>
    {
        public GetBasketHandlerTests(BasketContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private readonly BasketContextFactory _contextFactory;

        [Fact]
        public async Task handle_should_retrieve_a_new_record_and_return_it()
        {
            var handler = new GetBasketHandler(
                _contextFactory.BasketRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<BasketProfile>())),
                _contextFactory.CatalogService.Object);
            var result = await handler.Handle(
                new GetBasketRequest
                {
                    Id = new Guid("9ced6bfa-9659-462e-aece-49fe50613e96")
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.User.ShouldNotBeNull();
            result.ValidityDate.ShouldNotBeNull();
        }
    }
}