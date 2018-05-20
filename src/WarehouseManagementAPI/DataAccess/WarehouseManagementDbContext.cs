using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.DataAccess
{
    public class WarehouseManagementDbContext : DbContext
    {
        public WarehouseManagementDbContext(DbContextOptions<WarehouseManagementDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<PackageProduct> PackageProducts { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transport> Transports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PackageProduct>().HasKey(pp => new { pp.PackageId, pp.ProductId });
            builder.Entity<PackageProduct>()
                .HasOne(pp => pp.Package)
                .WithMany(p => p.PackageProducts)
                .HasForeignKey(pp => pp.PackageId);
            builder.Entity<PackageProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.PackageProducts)
                .HasForeignKey(pp => pp.ProductId);
            builder.Entity<PackageProduct>().ToTable("PackageProduct");

            builder.Entity<Package>().HasKey(p => p.PackageId);
            builder.Entity<Package>().ToTable("Package");

            builder.Entity<Order>().HasKey(o => o.OrderId);
            builder.Entity<Order>().ToTable("Order");

            builder.Entity<Customer>().HasKey(c => c.CustomerId);
            builder.Entity<Customer>().ToTable("Customer");

            builder.Entity<Product>().HasKey(p => p.ProductId);
            builder.Entity<Product>().ToTable("Product");

            builder.Entity<Transport>().HasKey(t => t.TransportId);
            builder.Entity<Transport>().ToTable("Transport");

            base.OnModelCreating(builder);
        }
    }
}
