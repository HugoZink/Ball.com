using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Models
{
    public class OrderProduct
    {
		public string ProductId { get; set; }
		public Product Product { get; set; }

		public string OrderId { get; set; }
		public Order Order { get; set; }

		public int Quantity { get; set; }
	}
}
