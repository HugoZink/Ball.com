using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Events
{
	public class DayHasBegan : Event
    {
		public DayHasBegan(Guid messageId) : base(messageId, MessageTypes.DayHasBegun)
		{
		}
	}
}
