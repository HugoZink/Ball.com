using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Events;
using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;
using ShippingService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingService
{
	public class EventHandler : IMessageHandlerCallback
	{
		private IMessagePublisher _messagePublisher;
		private ILogisticsService _logisticsService;
		private IMessageHandler _messageHandler;
		private string _connectionString;

		public EventHandler(IMessagePublisher messagePublisher, IMessageHandler messageHandler, ILogisticsService logisticsService,  string connectionString)
		{
			_connectionString = connectionString;
			_messagePublisher = messagePublisher;
			_messageHandler = messageHandler;
			_logisticsService = logisticsService;
		}

		public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			JObject messageObject = MessageSerializer.Deserialize(message);
			try
			{
				switch (messageType)
				{
					case MessageTypes.Unknown:
						break;
					case MessageTypes.PackageRegistered:
						await HandlePackageOrdersAsync(messageObject.ToObject<PackageRegistered>());
						break;
					case MessageTypes.DayHasBegun:
						await HandlePackagesAsync(messageObject.ToObject<DayHasBegun>());
						break;
				}
			}
			catch (Exception ex)
			{
				string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
				Console.WriteLine($"Error while handling {messageType} message with id {messageId}.\n{ex.ToString()}");
			}

			// always akcnowledge message - any errors need to be dealt with locally.
			return true;
		}

		private async Task<bool> HandlePackagesAsync(DayHasBegun e)
		{
			Console.WriteLine($"Day Has Begun " + e);

			using (var db = GetShippingDb())
			{
				try
				{
					var packageToShip = await db.Package.Where(p => p.TimeOfRecieve.Date == DateTime.Now.Date.AddDays(-1) && p.Shipped == false).ToListAsync(); ;
					var orders = await db.Order.ToListAsync();

					if (packageToShip != null && orders != null)
					{
						//var shippedOrders = new List<Order>();

						foreach (var pp in packageToShip)
						{
							var packageOrders = await db.Order.Where(o => o.PackageId == pp.PackageId).ToListAsync();

							foreach (var ppOrder in packageOrders)
							{
								var trackingCode = _logisticsService.GenerateTrackingCode();
								var ordersToShip = new Order { OrderId = ppOrder.OrderId, TrackingCode = trackingCode };

								//shippedOrders.Add(ordersToShip);
								// send event
								await _messagePublisher.PublishMessageAsync(MessageTypes.OrderShipped, ordersToShip, "");
								Console.WriteLine($"Order is shipped: " + ordersToShip);
							}

							var package = await db.Package.FirstOrDefaultAsync(t => t.PackageId == pp.PackageId);

							package.Shipped = true;

							db.Package.Update(package);

							await db.SaveChangesAsync();
						}
					}
				}
				catch (DbUpdateException)
				{
					Console.WriteLine($"Failed to ship order");
				}
			}

			return true;
		}

		private async Task<bool> HandlePackageOrdersAsync(PackageRegistered e)
		{
			Package package = Mapper.Map<Package>(e);

			using (var db = GetShippingDb())
			{
				try
				{
					await db.Package.AddAsync(package);

					await db.SaveChangesAsync();
				}
				catch (DbUpdateException)
				{
					Console.WriteLine($"Skipped adding package with package id {e.PackageId}.");
				}
			}

			Console.WriteLine($"Package added: " + e.PackageId);
			return true;
		}

		private ShippingDbContext GetShippingDb()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ShippingDbContext>();
			optionsBuilder.UseSqlServer(_connectionString);
			return new ShippingDbContext(optionsBuilder.Options);
		}
	}
}