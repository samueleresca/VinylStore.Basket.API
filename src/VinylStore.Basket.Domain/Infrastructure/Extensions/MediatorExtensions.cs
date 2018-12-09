using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VinylStore.Basket.Domain.Infrastructure.Extensions
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR();
            return services;
        }
    }
}