using System;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService.Events
{
	public class NewProductAdded : Event
	{
		public readonly string Id;
		public readonly string Name;

		public NewProductAdded(Guid messageId, string id, string name) : 
			base(messageId, MessageTypes.NewProductAdded)
		{
			Id = id;
			Name = name;
		}
	}
}
