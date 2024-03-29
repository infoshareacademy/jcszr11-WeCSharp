using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingAdminPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a4a698c-13c7-49fe-800d-5c6dd24d15ec", "AQAAAAIAAYagAAAAEOq7Oz9CZyoxQxlsQ/hEM51V5DsIvWTud8eZuWwIpAzkMsUEJnSfmP865ZIQZ+bLMw==", "c46a3e71-a556-4eb4-9354-b32c7f3507b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73337519-3c97-45a0-907d-5e389e2823db", null, "a5e19c4b-af82-4d0d-ba7d-d26edae16001" });
        }
    }
}
