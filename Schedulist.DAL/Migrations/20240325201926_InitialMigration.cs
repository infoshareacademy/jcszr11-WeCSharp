using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "85b46eb2-be6b-47db-9970-05c05d2e2e31", "f43a309d-4068-4fd0-ba30-022c46bf0043" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "04c47714-6d4f-43dc-a88b-573a6fb80f3a", "0bbeaf39-9b10-433c-80af-fa09fd3d2488" });
        }
    }
}
