using System.Collections.Generic;

namespace ShippingService.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string TrackingCode { get; set; }

        public List<Product> Products { get; set; }
    }
}