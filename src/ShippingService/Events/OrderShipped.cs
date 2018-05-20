using System;
using System.Collections.Generic;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Models;

namespace ShippingService.Events
{
    public class OrderShipped : Event
    {
		private readonly Order _order;

		public OrderShipped(Guid messageId, Order order) : base(messageId, MessageTypes.OrderShipped)
        {
			_order = order;
		}
    }
}