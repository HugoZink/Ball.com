﻿namespace ProductManagementAPI.Models
{

	public class Product
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public decimal WeightKg { get; set; }

		public ProductType Type { get; set; }
	}
}
