using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Infrastructure.Persistance.Migrations
{
    public partial class AddProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Perevozchik Moscow" },
                    { 2, "Aligator Company" },
                    { 3, "Gruzovichkoff" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
