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

		public string Id;
		public readonly string Name;
		public readonly decimal Price;
		public readonly float WeightKg;
		public readonly ProductType Type;

		public UpdateProduct(Guid messageId, string id, string name, decimal price, float weightkg, ProductType type) :
			base(messageId, MessageTypes.UpdateProduct)
		{
			Id = id;
			Name = name;
			Price = price;
			WeightKg = weightkg;
			Type = type;
		}

	}
}
