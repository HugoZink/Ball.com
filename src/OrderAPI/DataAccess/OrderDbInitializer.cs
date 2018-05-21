using OrderAPI.Model;
using OrderAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.DataAccess
{
    public class OrderDbInitializer
    {
        private OrderDbContext _dbContext;

        public OrderDbInitializer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            _dbContext.Database.EnsureCreated();

            if (!_dbContext.Products.Any())
            {
                var products = new Product[]
                {
                    new Product()
					{
						Name = "Television",
						Price = 750,
						WeightKg = 20
					},
					new Product()
					{
						Name = "Laptop",
						Price = 900,
						WeightKg = 2
					},
					new Product()
					{
						Name = "Network Switch",
						Price = 10,
						WeightKg = 0.5m
					}
				};

                _dbContext.AddRange(products);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
