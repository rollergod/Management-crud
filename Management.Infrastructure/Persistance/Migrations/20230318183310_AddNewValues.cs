using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Infrastructure.Persistance.Migrations
{
    public partial class AddNewValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Number",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "Number", "ProviderId" },
                values: new object[] { 1, new DateOnly(2023, 3, 18), "TestOrder", 1 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Name", "OrderId", "Quantity", "Unit" },
                values: new object[] { 1, "TestOrderItem", 1, 1m, "TestUnit" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Number",
                table: "Orders",
                column: "Number",
                unique: true);
        }
    }
}
