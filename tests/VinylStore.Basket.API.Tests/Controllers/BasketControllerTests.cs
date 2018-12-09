using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shouldly;
using VinylStore.Basket.Domain.Responses.Basket;
using VinylStore.Catalog.Fixtures;
using Xunit;

namespace VinylStore.Basket.API.Tests.Controllers
{
    public class BasketControllerTests : IClassFixture<BasketApplicationFactory<Startup>>
    {
        private readonly BasketApplicationFactory<Startup> _factory;

        public BasketControllerTests(BasketApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Theory]
        [InlineData("/api/basket/9ced6bfa-9659-462e-aece-49fe50613e96")]
        public async Task smoke_test_on_get(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            response.EnsureSuccessStatusCode();
        }
        
        [Theory]
        [InlineData("/api/basket/9ced6bfa-9659-462e-aece-49fe50613e96")]
        public async Task get_should_return_right_basket(string url)
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<BasketExtendedResponse>(responseContent);
          
            responseData.Id.ToString().ShouldBe("9ced6bfa-9659-462e-aece-49fe50613e96");
            responseData.Items.Count.ShouldNotBeNull();
            responseData.User.Email.ShouldNotBeEmpty();
            
            response.EnsureSuccessStatusCode();
        }
    }
}