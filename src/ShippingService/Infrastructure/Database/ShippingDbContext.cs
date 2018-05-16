using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShippingService.Models;

namespace ShippingService.Infrastructure.Database
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options)
        {
		}
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Logistics> Logistics { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(entity => entity.Id);
            builder.Entity<Order>().ToTable("Order");

            builder.Entity<Customer>().HasKey(entity => entity.Id);
            builder.Entity<Customer>().ToTable("Customer");

            builder.Entity<Logistics>().HasKey(entity => entity.Id);
            builder.Entity<Logistics>().ToTable("Logistics");            
            
            builder.Entity<Product>().HasKey(entity => entity.Id);
            builder.Entity<Product>().ToTable("Product");
            
            base.OnModelCreating(builder);

        }
        
    }
}