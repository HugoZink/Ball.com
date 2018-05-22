using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShippingService.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    TransportId = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    CountryOfDestination = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShippingCost = table.Column<decimal>(nullable: false),
                    TypeOfShipment = table.Column<string>(nullable: true),
                    WeightInKgMax = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.TransportId);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    PackageId = table.Column<string>(nullable: false),
                    BarcodeNumber = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Shipped = table.Column<bool>(nullable: false),
                    TimeOfRecieve = table.Column<DateTime>(nullable: false),
                    TransportId = table.Column<string>(nullable: true),
                    TypeOfPackage = table.Column<string>(nullable: true),
                    WeightInKgMax = table.Column<decimal>(nullable: false),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_Package_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Package_TransportId",
                table: "Package",
                column: "TransportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "Transport");
        }
    }
}
