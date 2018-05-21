using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

		public string State { get; set; }

		public List<OrderProduct> OrderProducts { get; set; }

		public bool AfterPayment { get; set; }

		[NotMapped]
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

		public Order()
		{
			this.OrderProducts = new List<OrderProduct>();
			this.State = OrderState.PENDING;
		}
    }

	public static class OrderState
	{
		public const string PENDING = "Pending";
		public const string PAYMENTINPROGRESS = "Payment In Progress";
		public const string PAYMENTCOMPLETE = "PaymentComplete";
		public const string AWAITINGAFTERPAYMENT = "AwaitingAfterPayment";
		public const string CLOSED = "Closed";
	}
}
