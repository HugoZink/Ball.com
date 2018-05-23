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

		public string Transport { get; set; }

		public List<Order> Orders { get; set; }

		// Ef needs a parameterless constructor
		public Package()
		{
		}

		public Package(string transport)
		{
			Shipped = false;
			Orders = new List<Order>();
			TimeOfRecieve = DateTime.Now;
			Transport = transport;
		}
	}
}
