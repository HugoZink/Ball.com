﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ShippingService.Infrastructure.Database;
using System;

namespace ShippingService.Migrations
{
    [DbContext(typeof(ShippingDbContext))]
    [Migration("20180523102833_packageOrders")]
    partial class packageOrders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShippingService.Models.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PackageId");

                    b.Property<string>("TrackingCode");

                    b.HasKey("OrderId");

                    b.HasIndex("PackageId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ShippingService.Models.Package", b =>
                {
                    b.Property<string>("PackageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarcodeNumber");

                    b.Property<string>("Region");

                    b.Property<bool>("Shipped");

                    b.Property<DateTime>("TimeOfRecieve");

                    b.Property<string>("Transport");

                    b.Property<string>("TypeOfPackage");

                    b.Property<decimal>("WeightInKgMax");

                    b.Property<string>("ZipCode");

                    b.HasKey("PackageId");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("ShippingService.Models.Order", b =>
                {
                    b.HasOne("ShippingService.Models.Package")
                        .WithMany("Orders")
                        .HasForeignKey("PackageId");
                });
#pragma warning restore 612, 618
        }
    }
}
