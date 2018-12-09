using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
                    ServiceDescriptor.Scoped(_ =>
                        _basketContextFactory.BasketRepository.Object));
                services.Replace(ServiceDescriptor.Scoped(_ =>
                    _basketContextFactory.CatalogService.Object)
                );
            });
        }
    }
}