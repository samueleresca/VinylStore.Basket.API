﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace VinylStore.Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5002")
                .UseStartup<Startup>();
        }
    }
}