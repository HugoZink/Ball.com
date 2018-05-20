using System;
using System.IO;
using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pitstop.Infrastructure.Messaging;
using Polly;
using ShippingService.Commands;
using ShippingService.Events;
using ShippingService.Infrastructure.Database;
using ShippingService.Infrastructure.Repositories;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService
{
    internal static class Program
    {
        private static readonly string Env;
        private static IConfigurationRoot Config { get; set; }

        static Program()
        {
            Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

			Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Env}.json", optional: false)
                .Build();
        }

        private static void Main(string[] args)
        {
            // get configuration
            var configSection = Config.GetSection("RabbitMQ");
            var host = configSection["Host"];
            var userName = configSection["UserName"];
            var password = configSection["Password"];
            var sqlConnectionString = Config.GetConnectionString("ShippingCN");

            var services = new ServiceCollection();

            services.AddDbContext<ShippingDbContext>(options => options.UseSqlServer(sqlConnectionString));

            //...add any other services needed
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ILogisticsRepository, LogisticsRepository>();
            
            services.AddTransient<IMessageHandlerCallback, EventHandler>();
            
            services.AddTransient<IMessagePublisher>((sp) =>
                new RabbitMQMessagePublisher(host, userName, password, "Ball.com"));
            services.AddSingleton<IMessageHandler>((sp) =>
                new RabbitMQMessageHandler(host, userName, password, "Ball.com", "Shipping", ""));

            // then build provider 
            var serviceProvider = services.BuildServiceProvider();

            var messageHandlerCallback = serviceProvider.GetService<IMessageHandlerCallback>();
            var messageHandler = serviceProvider.GetService<IMessageHandler>();

            var dbContext = serviceProvider.GetService<ShippingDbContext>();

            //Policy
            //    .Handle<Exception>()
            //    .WaitAndRetry(5, r => TimeSpan.FromSeconds(5),
            //        (ex, ts) => { Console.WriteLine("Error connecting to DB. Retrying in 5 sec."); })
            //    .Execute(() => dbContext.Database.Migrate());

			messageHandler.Start(messageHandlerCallback);

            if (Env == "Development")
            {
                Console.WriteLine("Shipping service started. Press any key to stop...");
                Console.ReadKey(true);
                messageHandler.Stop();
            }
            else
            {
                Console.WriteLine("Shipping service started.");
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
        }

		private static void SetupAutoMapper()
		{
			// setup automapper
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<OrderShipped, Order>();
				cfg.CreateMap<ShipOrder, OrderShipped>();
			});
		}

	}
}