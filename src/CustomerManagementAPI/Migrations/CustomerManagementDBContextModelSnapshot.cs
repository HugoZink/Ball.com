﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Pitstop.CustomerManagementAPI.DataAccess;
using Pitstop.CustomerManagementAPI.Model;
using System;

namespace Pitstop.CustomerManagementAPI.Migrations
{
    [DbContext(typeof(CustomerManagementDBContext))]
    partial class CustomerManagementDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pitstop.CustomerManagementAPI.Model.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.Property<string>("PostalCode");

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Pitstop.CustomerManagementAPI.Model.SupportMessage", b =>
                {
                    b.Property<string>("SupportMessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("CustomerId");

                    b.Property<int>("MessageType");

                    b.HasKey("SupportMessageId");

                    b.HasIndex("CustomerId");

                    b.ToTable("SupportMessage");
                });

            modelBuilder.Entity("Pitstop.CustomerManagementAPI.Model.SupportMessage", b =>
                {
                    b.HasOne("Pitstop.CustomerManagementAPI.Model.Customer")
                        .WithMany("SupportMessages")
                        .HasForeignKey("CustomerId");
                });
#pragma warning restore 612, 618
        }
    }
}
