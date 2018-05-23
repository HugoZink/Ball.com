using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShippingService.Migrations
{
    public partial class changeTransportToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Transport_TransportId",
                table: "Package");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropIndex(
                name: "IX_Package_TransportId",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "TransportId",
                table: "Package",
                newName: "Transport");

            migrationBuilder.AlterColumn<string>(
                name: "Transport",
                table: "Package",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Transport",
                table: "Package",
                newName: "TransportId");

            migrationBuilder.AlterColumn<string>(
                name: "TransportId",
                table: "Package",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Package_TransportId",
                table: "Package",
                column: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Transport_TransportId",
                table: "Package",
                column: "TransportId",
                principalTable: "Transport",
                principalColumn: "TransportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
