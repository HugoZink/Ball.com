using System;
using System.Collections.Generic;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Models;

namespace ShippingService.Commands
{
    public class ShipOrder : Command
    {
		private readonly string _trackingCode;
		private readonly string _id;
		private readonly List<Product> _products;

		public ShipOrder(Guid messageId, MessageTypes messageType, string trackingCode, string id, List<Product> products) : base(messageId, messageType)
        {
			_id = id;
			_trackingCode = trackingCode;
			_products = products;
		}
    }
}