using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Objectbay.Vacation.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VacationItems",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("3fb362d5-18c3-45de-813e-6147adbc3d9a"), 0, "Badehose" },
                    { new Guid("c37f373d-6869-48a2-a8c0-2e3b2626da6a"), 3, "Sonnencreme" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VacationItems",
                keyColumn: "Id",
                keyValue: new Guid("3fb362d5-18c3-45de-813e-6147adbc3d9a"));

            migrationBuilder.DeleteData(
                table: "VacationItems",
                keyColumn: "Id",
                keyValue: new Guid("c37f373d-6869-48a2-a8c0-2e3b2626da6a"));
        }
    }
}