using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Commands;
using ShippingService.Events;
using ShippingService.Infrastructure.Database;
using ShippingService.Infrastructure.Repositories;
using ShippingService.Infrastructure.Services;
using ShippingService.Models;
using ShippingService.Repositories;
using ShippingService.Services;
using System;
using System.IO;
using System.Threading;

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
			//services.AddTransient<IProductRepository, ProductRepository>();
			//services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<IOrderRepository, EFOrderRepository>();
			//services.AddTransient<ILogisticsRepository, LogisticsRepository>();
			services.AddTransient<IPackageRepository, EFPackageRepository>();

			services.AddTransient<ILogisticsService, LogisticsService>();

			services.AddTransient<IMessageHandlerCallback, EventHandler>();

			services.AddTransient<IMessagePublisher>((sp) =>
				new RabbitMQMessagePublisher(host, userName, password, "Ball.com"));
			services.AddSingleton<IMessageHandler>((sp) =>
				new RabbitMQMessageHandler(host, userName, password, "Ball.com", "Shipping", ""));

			// then build provider 
			var serviceProvider = services.BuildServiceProvider();

			//var messageHandlerCallback = serviceProvider.GetService<IMessageHandlerCallback>();
			RabbitMQMessageHandler messageHandler = new RabbitMQMessageHandler(host, userName, password, "Ball.com", "OrderAPI", "");

			var dbContext = serviceProvider.GetService<ShippingDbContext>();
			var messagePublisher = serviceProvider.GetService<IMessagePublisher>();
			var logisticsService = serviceProvider.GetService<ILogisticsService>();

			SetupAutoMapper();

			//Policy
			//    .Handle<Exception>()
			//    .WaitAndRetry(5, r => TimeSpan.FromSeconds(5),
			//        (ex, ts) => { Console.WriteLine("Error connecting to DB. Retrying in 5 sec."); })
			//    .Execute(() => dbContext.Database.Migrate());

			// start event-handler
			var eventHandler = new EventHandler(messagePublisher, messageHandler, logisticsService, Config.GetConnectionString("ShippingCN"));
			eventHandler.Start();

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
				cfg.CreateMap<ShipOrder, OrderShipped>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(c => Guid.NewGuid()));
				cfg.CreateMap<Package, PackageRegistered>();
				cfg.CreateMap<Order, OrderShipped>();
				cfg.CreateMap<PackageRegistered, Package>()
					.ForCtorParam("transport", opt => opt.ResolveUsing(c => c.Transport.TransportId)); ;
			});
		}

	}
}