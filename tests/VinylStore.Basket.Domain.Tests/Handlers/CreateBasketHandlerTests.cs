using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using VinylStore.Basket.Domain.Handlers.Basket;
using VinylStore.Basket.Domain.Infrastructure.Mapper;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using Xunit;

namespace VinylStore.Basket.Domain.Tests.Handlers
{
    public class CreateBasketHandlerTests
    {
        public CreateBasketHandlerTests()
        {
            _memoryCollection = new Dictionary<Guid, string>();
            _basketRepository = new Mock<IBasketRepository>();

            _basketRepository
                .Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                    Task.FromResult(JsonConvert.DeserializeObject<Entities.Basket>(_memoryCollection[id])));

            _basketRepository
                .Setup(x => x.AddOrUpdateAsync(It.IsAny<Entities.Basket>()))
                .Callback((Entities.Basket x) => { _memoryCollection[x.Id] = JsonConvert.SerializeObject(x); })
                .ReturnsAsync((Entities.Basket x) =>
                    JsonConvert.DeserializeObject<Entities.Basket>(_memoryCollection[x.Id]));
        }

        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly Dictionary<Guid, string> _memoryCollection;


        [Fact]
        public async Task handle_should_create_a_new_record_and_return()
        {
            var handler = new CreateBasketHandler(_basketRepository.Object,
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<BasketProfile>())));
            var result = await handler.Handle(
                new CreateBasketRequest
                {
                    ItemsIds = new List<string> {"test", "test"},
                    UserEmail = "samuele.resca@gmail.com",
                    ValidityDate = DateTime.UtcNow.ToString()
                }, CancellationToken.None);

            result.Id.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.User.ShouldNotBeNull();
            result.ValidityDate.ShouldBeNull();
        }
    }
}