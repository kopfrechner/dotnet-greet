using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Objectbay.Vacation.WebApi.Migrations {
    /// <inheritdoc />
    public partial class AddVacationEntity : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<Guid>(
                name: "VacationId",
                table: "VacationItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vacation",
                columns: table => new {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Vacation", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "VacationItems",
                keyColumn: "Id",
                keyValue: new Guid("3fb362d5-18c3-45de-813e-6147adbc3d9a"),
                column: "VacationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "VacationItems",
                keyColumn: "Id",
                keyValue: new Guid("c37f373d-6869-48a2-a8c0-2e3b2626da6a"),
                column: "VacationId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_VacationItems_VacationId",
                table: "VacationItems",
                column: "VacationId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationItems_Vacation_VacationId",
                table: "VacationItems",
                column: "VacationId",
                principalTable: "Vacation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationItems_Vacation_VacationId",
                table: "VacationItems");

            migrationBuilder.DropTable(
                name: "Vacation");

            migrationBuilder.DropIndex(
                name: "IX_VacationItems_VacationId",
                table: "VacationItems");

            migrationBuilder.DropColumn(
                name: "VacationId",
                table: "VacationItems");
        }
    }
}