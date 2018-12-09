using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using VinylStore.Basket.Domain.Responses.Basket;
using VinylStore.Catalog.Fixtures;
using VinylStoreBasket.Fixtures;
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

        [Theory]
        [LoadTestData("record-data.json", "item_without_id")]
        public async Task post_should_create_a_basket(object jsonPayload)
        {
            var client = _factory.CreateClient();

            var httpContent = new StringContent(jsonPayload.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/basket", httpContent);

            response.EnsureSuccessStatusCode();

            var responseHeader = response.Headers.Location;
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            responseHeader.ToString().Contains("/api/basket").ShouldBeTrue();
        }

        [Theory]
        [InlineData("9ced6bfa-9659-462e-aece-49fe50613e96", "f5da5ce4-091e-492e-a70a-22b073d75a52")]
        public async Task put_should_should_increase_basket_quantity(string basketId, string basketItemId)
        {
            var client = _factory.CreateClient();

            var response = await client.PutAsync($"/api/basket/{basketId}/items/{basketItemId}",
                new StringContent(string.Empty));

            response.EnsureSuccessStatusCode();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("9ced6bfa-9659-462e-aece-49fe50613e96", "f5da5ce4-091e-492e-a70a-22b073d75a52")]
        public async Task delete_should_should_decrease_basket_quantity(string basketId, string basketItemId)
        {
            var client = _factory.CreateClient();

            var response = await client.PutAsync($"/api/basket/{basketId}/items/{basketItemId}",
                new StringContent(string.Empty));

            response.EnsureSuccessStatusCode();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}