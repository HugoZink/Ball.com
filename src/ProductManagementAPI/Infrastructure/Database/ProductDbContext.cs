using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Database
{
	public class ProductDbContext : DbContext
	{

		public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
		{
			Database.Migrate();
		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{

			builder.Entity<Product>().HasKey(m => m.Id);
			builder.Entity<Product>().ToTable("Product");
			base.OnModelCreating(builder);

		}

	}
}
