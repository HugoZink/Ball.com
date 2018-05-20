using Pitstop.Infrastructure.Messaging;
using ShippingService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Events
{
	public class OrderPackaged : Event
	{

		public readonly Order _order;

		public OrderPackaged(Guid messageId, MessageTypes messageType, Order order) : base(messageId, messageType)
		{
			_order = order;
		}
	}
}
