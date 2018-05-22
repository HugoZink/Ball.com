using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Models;
using System;

namespace ProductManagementAPI.Infrastructure.Events
{
	public class NewProductAdded : Event
	{
		public readonly string Id;
		public readonly string Name;
		public readonly decimal Price;
		public readonly decimal WeightKg;
		public readonly ProductType Type;

		public NewProductAdded(Guid messageId, string id, string name, decimal price, decimal weightkg, ProductType type) : 
			base(messageId, MessageTypes.NewProductAdded)
		{
			Id = id;
			Name = name;
			Price = price;
			WeightKg = weightkg;
			Type = type;
		}
	}
}
