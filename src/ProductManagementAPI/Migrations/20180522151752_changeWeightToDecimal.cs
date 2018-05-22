using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProductManagementAPI.Migrations
{
    public partial class changeWeightToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WeightKg",
                table: "Product",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "WeightKg",
                table: "Product",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
