using ProductManagementAPI.Database;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Database
{
    public class ProductDbSeeder
    {
		public static void Seed(ProductDbContext dbContext)
		{
			dbContext.Database.EnsureCreated();

			// Look for Any existing Data
			if (dbContext.Products.Any())
			{
				return; // Database has been Seeded
			}

			var products = new Product[]
			{
				new Product {Name = "Phone", Price = 1.00m, Type = ProductType.Electronic},
				new Product {Name = "Chair", Price = 2.00m, Type = ProductType.None},
				new Product {Name = "Dino", Price = 3.00m, Type = ProductType.Toy}
			};

			foreach (var product in products)
			{
				dbContext.Products.Add(product);
			}

			dbContext.SaveChanges();
		}
	}
}
