using System.Collections.Generic;
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
    public class CreateBasketHandlerTests : IClassFixture<BasketContextFactory>
    {
        public CreateBasketHandlerTests(BasketContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private readonly BasketContextFactory _contextFactory;

        [Fact]
        public async Task handle_should_create_a_new_record_and_return()
        {
            var handler = new CreateBasketHandler(
                _contextFactory.BasketRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<BasketProfile>())),
                _contextFactory.CatalogService.Object);

            var result = await handler.Handle(
                new CreateBasketRequest
                {
                    ItemsIds = new List<string>
                        {"be05537d-5e80-45c1-bd8c-aa21c0f1251e", "f5da5ce4-091e-492e-a70a-22b073d75a52"},
                    UserEmail = "samuele.resca@gmail.com"
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.User.ShouldNotBeNull();
            result.ValidityDate.ShouldNotBeNull();
        }
    }
}