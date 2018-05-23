using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingService.Models
{
    public class Order
    {
		public string OrderId { get; set; }

		public string PackageId { get; set; }

		public string TrackingCode { get; set; }

		public Order()
		{

		}
	}
}