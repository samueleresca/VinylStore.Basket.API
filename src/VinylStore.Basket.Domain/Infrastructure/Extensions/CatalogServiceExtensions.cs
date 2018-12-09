using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VinylStore.Basket.Domain.Infrastructure.CatalogEnricher;
using VinylStore.Basket.Domain.Infrastructure.Services;
using VinylStore.Catalog.API.Client;

namespace VinylStore.Basket.Domain.Infrastructure.Extensions
{
    public static class CatalogServiceExtensions
    {
        public static IServiceCollection AddCatalogService(this IServiceCollection services, Uri uri)
        {
            services.AddScoped<ICatalogClient>(x => new CatalogClient(uri));
            services.AddScoped<ICatalogService, CatalogService>();

            return services;
        }
    }
}