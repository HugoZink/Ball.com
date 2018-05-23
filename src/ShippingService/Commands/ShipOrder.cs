using System;
using System.Collections.Generic;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Models;

namespace ShippingService.Commands
{
    public class ShipOrder : Command
    {
		private readonly string _id;
		private readonly Order _order;

		public ShipOrder(Guid messageId, MessageTypes messageType, string id, Order order) : base(messageId, messageType)
        {
			_id = id;
			_order = order;
		}
    }
}