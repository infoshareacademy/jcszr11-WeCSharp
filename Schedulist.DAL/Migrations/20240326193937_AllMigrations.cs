using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AllMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "73337519-3c97-45a0-907d-5e389e2823db", "a5e19c4b-af82-4d0d-ba7d-d26edae16001" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "85b46eb2-be6b-47db-9970-05c05d2e2e31", "f43a309d-4068-4fd0-ba30-022c46bf0043" });
        }
    }
}
