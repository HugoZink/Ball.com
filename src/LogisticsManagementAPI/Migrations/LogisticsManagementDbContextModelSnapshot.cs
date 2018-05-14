﻿// <auto-generated />
using LogisticsManagementAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LogisticsManagementAPI.Migrations
{
    [DbContext(typeof(LogisticsManagementDbContext))]
    partial class LogisticsManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LogisticsManagementAPI.Models.Transport", b =>
                {
                    b.Property<string>("TransportId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<string>("CountryOfDestination");

                    b.Property<string>("Description");

                    b.Property<decimal>("ShippingCost");

                    b.Property<string>("TypeOfShipment");

                    b.Property<decimal>("WeightInKgMax");

                    b.HasKey("TransportId");

                    b.ToTable("Transport");
                });
#pragma warning restore 612, 618
        }
    }
}
