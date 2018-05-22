using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitstop.TimeService.Events
{
	public class DayHasBegun : Event
	{
		public DayHasBegun(Guid messageId) : base(messageId, MessageTypes.DayHasBegun)
		{
		}
	}
}
