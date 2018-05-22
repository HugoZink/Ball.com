using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingService.Models
{
    public class Order
    {
		public string OrderId { get; set; }

		public string TrackingCode { get; set; }

		public List<OrderProduct> OrderProducts { get; set; }

		[NotMapped]
		public IEnumerable<Product> Products
		{
			get
			{
				var products = new List<Product>();
				foreach (OrderProduct op in OrderProducts)
				{
					products.Add(op.Product);
				}

				return products;
			}
		}
	}
}