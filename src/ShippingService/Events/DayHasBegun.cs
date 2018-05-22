using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Events
{
	public class DayHasBegun : Event
    {
		public DayHasBegun(Guid messageId) : base(messageId, MessageTypes.DayHasBegun)
		{
		}
	}
}
