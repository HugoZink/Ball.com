using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Models;
using System;

namespace ProductManagementAPI.Infrastructure.Commands
{
	public class AddProduct : Command
	{
		public readonly string Id;
		public readonly string Name;
		public readonly decimal Price;
		public readonly ProductType Type;

		public AddProduct(Guid messageId, string id, string name, decimal price, ProductType type) : 
			base(messageId, MessageTypes.AddProduct)
		{
			Id = id;
			Name = name;
			Price = price;
			Type = type;
		}
	}
}
