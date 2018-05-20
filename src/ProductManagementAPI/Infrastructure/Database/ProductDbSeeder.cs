using ProductManagementAPI.Database;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Database
{
	public static class ProductDbSeeder
	{

		public static void Seed(ProductDbContext _dbContext)
		{

			_dbContext.Database.EnsureCreated();

			// Look for Any existing Data
			if (_dbContext.Products.Any())
			{
				return; // Database has been Seeded
			}

			var products = new Product[]
			{

				new Product {Name = "Phone", Price = 1.00m, WeightKg= 2, Type = ProductType.Electronic},
				new Product {Name = "Chair", Price = 2.00m, WeightKg= 20, Type = ProductType.None},
				new Product {Name = "Dino", Price = 3.00m, WeightKg= 1, Type = ProductType.Toy}

			};

			_dbContext.AddRange(products);

			_dbContext.SaveChanges();
		}
	}
}
