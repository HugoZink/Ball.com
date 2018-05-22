using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Events
{
	public class OrderPayed : Event
	{
		public OrderPayed(Guid messageId, MessageTypes messageType) : base(messageId, MessageTypes.OrderPayed)
		{
		}
	}
}
