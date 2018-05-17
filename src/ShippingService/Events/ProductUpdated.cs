using System;
using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Models;

namespace ShippingService.Events
{
    public class ProductUpdated : Event
    {
	    public readonly string Id;
	    public readonly string Name;

		public ProductUpdated(Guid messageId, string id, string name) :
			base(messageId, MessageTypes.ProductUpdated)
		{
			Id = id;
			Name = name;
		}
	}
}
