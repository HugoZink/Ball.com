using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingService.Models
{
	public class Package
	{
		public string PackageId { get; set; }
		public string TypeOfPackage { get; set; }
		public string Region { get; set; }
		public string BarcodeNumber { get; set; }
		public string ZipCode { get; set; }
		public decimal WeightInKgMax { get; set; }
		public DateTime TimeOfRecieve { get; set; }
		public bool Shipped { get; set; }

		public Transport Transport { get; set; }

		[NotMapped]
		public List<Order> Orders { get; set; }

		public Package()
		{
			Shipped = false;
			Orders = new List<Order>();
			TimeOfRecieve = new DateTime();
		}
	}
}
