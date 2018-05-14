using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Commands
{
    public class UpdateProduct : Command
	{

		public readonly string Id;
		public readonly string Name;
		public readonly decimal Price;
		public readonly ProductType Type;

		public UpdateProduct(Guid messageId, string id, string name, decimal price, ProductType type) :
			base(messageId, MessageTypes.UpdateProduct)
		{
			Id = id;
			Name = name;
			Price = price;
			Type = type;
		}

	}
}
