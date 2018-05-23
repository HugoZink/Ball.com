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

		public DbSet<Package> Package { get; set; }
		public DbSet<Order> Order { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

	}
}