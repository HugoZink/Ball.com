using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pitstop.CustomerManagementAPI.Migrations
{
    public partial class CustomerSupportMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportMessage",
                columns: table => new
                {
                    SupportMessageId = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    MessageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportMessage", x => x.SupportMessageId);
                    table.ForeignKey(
                        name: "FK_SupportMessage_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessage_CustomerId",
                table: "SupportMessage",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportMessage");
        }
    }
}
