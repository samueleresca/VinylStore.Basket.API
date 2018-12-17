using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using VinylStore.Cart.Domain.Infrastructure.Services;
using VinylStore.Cart.Infrastructure.Services;
using VinylStore.Catalog.API.Client;

namespace VinylStore.Cart.Infrastructure
{
    public static class CatalogServiceExtensions
    {
        public static IServiceCollection AddCatalogService(this IServiceCollection services, Uri uri)
        {
            services.AddScoped<ICatalogService, CatalogService>();

            services.AddHttpClient<ICatalogClient, CatalogClient>(client =>
                {
                    client.BaseAddress = uri;
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Set lifetime to five minutes
                .AddPolicyHandler(RetryPolicy())
                .AddPolicyHandler(CircuitBreakerPolicy());
            
            return services;
        }
        
        static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                    retryAttempt)));
        }
        
        static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}