using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Infrastructure.Database
{
	class ShippingDbContextFactory : IDesignTimeDbContextFactory<ShippingDbContext>
	{
		public ShippingDbContext CreateDbContext(string[] args)
		{
			DbContextOptionsBuilder<ShippingDbContext> optionsBuilder = new DbContextOptionsBuilder<ShippingDbContext>();

			optionsBuilder.UseSqlServer("Server=.\\localhost,1434;user id=sa;password=8jkGh47hnDw89Haq8LN2;database=Shipping;");

			return new ShippingDbContext(optionsBuilder.Options);
		}
	}
}