using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using StackExchange.Redis;
using VinylStore.Basket.Domain.Infrastructure.CatalogEnricher;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Infrastructure.Services;
using VinylStore.Basket.Infrastructure.Repositories;
using VinylStoreBasket.Fixtures;

namespace VinylStore.Catalog.Fixtures
{
    public class BasketApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly BasketContextFactory _basketContextFactory;

        public BasketApplicationFactory()
        {
            _basketContextFactory = new BasketContextFactory();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.Replace(
                    ServiceDescriptor.Scoped<IBasketRepository>(_ =>
                       _basketContextFactory.BasketRepository.Object));
                services.Replace(        ServiceDescriptor.Scoped<ICatalogService>(_ =>
                    _basketContextFactory.CatalogService.Object)
                    );
        
            });
        }
    }
}