using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VinylStore.Basket.API.Infrastructure.Configurations;
using VinylStore.Basket.Domain.Infrastructure.CatalogEnricher;
using VinylStore.Basket.Domain.Infrastructure.Extensions;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Infrastructure.Services;
using VinylStore.Basket.Infrastructure.Repositories;

namespace VinylStore.Basket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.Configure<BasketDataSourceSettings>(Configuration);

            services
                .AddMediator()
                .AddCatalogService(new Uri("https://testapi.com"))
                .AddAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}