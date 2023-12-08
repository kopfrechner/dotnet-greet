using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Objectbay.Vacation.WebApi.Migrations {
    /// <inheritdoc />
    public partial class Seed_update : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.UpdateData(
                table: "VacationItems",
                keyColumn: "Id",
                keyValue: new Guid("3fb362d5-18c3-45de-813e-6147adbc3d9a"),
                column: "Name",
                value: "Bikini");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.UpdateData(
                table: "VacationItems",
                keyColumn: "Id",
                keyValue: new Guid("3fb362d5-18c3-45de-813e-6147adbc3d9a"),
                column: "Name",
                value: "Badehose");
        }
    }
}