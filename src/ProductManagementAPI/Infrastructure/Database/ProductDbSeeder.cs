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
		private ProductDbContext _dbContext;

		public ProductDbSeeder(ProductDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Seed()
		{
			if (!_dbContext.Products.Any())
			{

				_dbContext.Database.EnsureCreated();

				// Look for Any existing Data
				if (_dbContext.Products.Any())
				{
					return; // Database has been Seeded
				}

				var products = new Product[]
				{

				new Product {Name = "Phone", Price = 1.00m, Type = ProductType.Electronic},
				new Product {Name = "Chair", Price = 2.00m, Type = ProductType.None},
				new Product {Name = "Dino", Price = 3.00m, Type = ProductType.Toy}

				};

				_dbContext.AddRange(products);

				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
