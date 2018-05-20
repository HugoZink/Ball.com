using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShippingService.Models;

namespace ShippingService.Infrastructure.Database
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options)
        {
				Database.Migrate();
		}
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Logistics> Logistics { get; set; }

        public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderProduct>()
				.HasKey(t => new { t.OrderId, t.ProductId });
			
			base.OnModelCreating(modelBuilder);
		}

	}
}