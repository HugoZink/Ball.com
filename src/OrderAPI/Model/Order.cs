using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Model
{
    public class Order
    {
		public string OrderId { get; set; }

		public Customer Customer { get; set; }

		public DateTime DateTime { get; set; }

		public string TrackingCode { get; set; }

		public OrderState State { get; set; }

		public List<OrderProduct> OrderProducts { get; set; }

		public IEnumerable<Product> Products
		{
			get
			{
				var products = new List<Product>();
				foreach(OrderProduct op in OrderProducts)
				{
					products.Add(op.Product);
				}

				return products;
			}
		}
    }

	public enum OrderState
	{
		Pending,
		PaymentInProgress,
		PaymentComplete,
		AwaitingAfterPayment,
		Closed
	}
}
