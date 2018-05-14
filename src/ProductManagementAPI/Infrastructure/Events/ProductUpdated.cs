using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Events
{
    public class ProductUpdated : Event
    {
		public readonly string Id;
		public readonly string Name;
		public readonly decimal Price;
		public readonly float WeightKg;
		public readonly ProductType Type;

		public ProductUpdated(Guid messageId, string id, string name, decimal price, float weightkg, ProductType type) :
			base(messageId, MessageTypes.ProductUpdated)
		{
			Id = id;
			Name = name;
			Price = price;
			WeightKg = weightkg;
			Type = type;
		}
	}
}
