using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VinylStore.Cart.Domain.Infrastructure.Extensions;
using VinylStore.Cart.Domain.Infrastructure.Repositories;
using VinylStore.Cart.Domain.Infrastructure.Services;
using VinylStore.Cart.Infrastructure;
using VinylStore.Cart.Infrastructure.Configurations;
using VinylStore.Cart.Infrastructure.Extensions;
using VinylStore.Cart.Infrastructure.Repositories;
using VinylStore.Cart.Infrastructure.Services;

namespace VinylStore.Cart.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.Configure<CartDataSourceSettings>(Configuration);


            services
                .AddMediator()
                .AddCatalogService(new Uri(Configuration["CatalogApiUrl"]))
                .AddAutoMapper()
                .AddRabbitMQ("Test123", "host=localhost:5672;username=guest;password=guest", CurrentEnvironment.EnvironmentName);
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