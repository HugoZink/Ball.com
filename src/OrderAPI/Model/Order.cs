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

		public List<Product> Products { get; set; }
    }
}
