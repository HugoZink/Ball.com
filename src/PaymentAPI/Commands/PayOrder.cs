using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Commands
{
	public class PayOrder : Command
	{
		public PayOrder(Guid messageId, MessageTypes messageType) : base(messageId, MessageTypes.PayOrder)
		{
		}
	}
}
