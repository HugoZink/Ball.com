using AutoMapper.Configuration;
using LogisticsManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.DataAccess
{
    public class LogisticsManagementDbContext : DbContext
    {

        public LogisticsManagementDbContext(DbContextOptions<LogisticsManagementDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Transport> Transports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transport>().HasKey(m => m.TransportId);
            builder.Entity<Transport>().ToTable("Transport");
            base.OnModelCreating(builder);
        }
    }
}
