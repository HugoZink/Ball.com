using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Model;

namespace OrderAPI.DataAccess
{
    public class OrderDbContext : DbContext
    {
		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
			Database.Migrate();
		}

		public DbSet<Order> Orders { get; set; }

		public DbSet<Customer> Customers { get; set; }
	}
}
