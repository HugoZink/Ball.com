using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Events
{
	public class OrderPayed : Event
	{
		readonly string _orderId;
		readonly string _bank;

		public OrderPayed(Guid messageId, string orderId, string bank) : base(messageId, MessageTypes.OrderPayed)
		{
			_orderId = orderId;
			_bank = bank;
		}
	}
}
