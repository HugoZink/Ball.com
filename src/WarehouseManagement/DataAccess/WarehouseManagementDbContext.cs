using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Models;

namespace WarehouseManagement.DataAccess
{
    public class WarehouseManagementDbContext : DbContext
    {

        public WarehouseManagementDbContext(DbContextOptions<WarehouseManagementDbContext> options)
        {
            Database.Migrate();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transport> Transports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
