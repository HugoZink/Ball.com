using AutoMapper;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Events;
using ShippingService.Models;
using ShippingService.Repositories;
using ShippingService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingService
{
	public class EventHandler : IMessageHandlerCallback
	{
		private IMessagePublisher _messagePublisher;
		private ILogisticsService _logisticsService;
		private IPackageRepository _packageRepository;

		public EventHandler(IMessagePublisher messagePublisher, ILogisticsService logisticsService, IPackageRepository packageRepository)
		{
			_logisticsService = logisticsService;
			_packageRepository = packageRepository;
			_messagePublisher = messagePublisher;

		}

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			var messageObject = MessageSerializer.Deserialize(message);

			switch (messageType)
			{
				case MessageTypes.Unknown:
					break;
				case MessageTypes.OrderPackeged:
					await HandlePackageOrdersAsync(messageObject.ToObject<PackageRegistered>());
					break;
				case MessageTypes.DayHasBegun:
					await HandlePackagesAsync(messageObject.ToObject<DayHasBegan>());
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null);
			}

			// always akcnowledge message - any errors need to be dealt with locally.
			return true;
		}

		private async Task<bool> HandlePackagesAsync(DayHasBegan e)
		{
			Console.WriteLine($"Day Has Began " + e);

			var packageToShip = await _packageRepository.GetPackagesFromYesterdayAsync();

			var shippedOrders = new List<OrderShipped>();

			foreach (var pp in packageToShip)
			{

				foreach (var ppOrder in pp.Orders)
				{
					var trackingCode = _logisticsService.GenerateTrackingCode();
					var ordersToShip = new Order { OrderId = ppOrder.OrderId, OrderProducts = ppOrder.OrderProducts, TrackingCode = trackingCode };

					shippedOrders.Add(Mapper.Map<OrderShipped>(ordersToShip));
					Console.WriteLine($"Order is shipped: " + ordersToShip);
				}

				await _packageRepository.SetPackageToShippedAsync(pp.PackageId);
			}

			// send event
			await _messagePublisher.PublishMessageAsync(MessageTypes.OrderShipped, shippedOrders, "");

			return true;
		}

		private async Task<bool> HandlePackageOrdersAsync(PackageRegistered e)
		{
			Package package = Mapper.Map<Package>(e);

			await _packageRepository.AddPackageAsync(package);
			Console.WriteLine($"Package added: " + e);
			return true;
		}
	}
}