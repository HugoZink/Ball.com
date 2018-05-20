using System.Collections.Generic;

namespace ShippingService.Models
{
    public class Order
    {
		public string OrderId { get; set; }

        public string TrackingCode { get; set; }
	    
	    public Customer Customer { get; set; }

		public ICollection<OrderProduct> OrderProducts { get; set; }

		public List<Product> Products
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
}