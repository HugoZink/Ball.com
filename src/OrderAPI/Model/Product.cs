using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Model
{
    public class Product
    {
		public string ProductId { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public decimal WeightKg { get; set; }
	}
}
