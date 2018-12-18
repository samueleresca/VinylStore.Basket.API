using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace VinylStore.Cart.Fixtures
{
    public class CartApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly CartContextFactory _cartContextFactory;

        public CartApplicationFactory()
        {
            _cartContextFactory = new CartContextFactory();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.Replace(
                    ServiceDescriptor.Scoped(_ =>
                        _cartContextFactory.GetCartRepository()));
                services.Replace(ServiceDescriptor.Scoped(_ =>
                    _cartContextFactory.GetCatalogService())
                );
            });
        }
    }
}