using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Model
{
	/// <summary>
	/// Specifies a link between a product and an order.
	/// </summary>
    public class OrderProduct
    {
		//This class is a workaround for the lack of n:m relationships in Entity Framework Core.

		public string OrderId { get; set; }
		public Order Order { get; set; }

		public string ProductId { get; set; }
		public Product Product { get; set; }

		public int Quantity { get; set; }
    }
}
