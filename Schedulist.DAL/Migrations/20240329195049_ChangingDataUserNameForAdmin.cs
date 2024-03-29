using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangingDataUserNameForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f3c9abc8-66f0-446b-8e26-89aa479fb1b2", "AQAAAAIAAYagAAAAEPi+5dq89aiqazJ2y8C2wst5NqfavqpPIGR5CjOOg3p5aglHt6NNvYtKm7UuoCoEOA==", "045b1d32-a16a-4a0d-8bd6-11cf196f3f74", "KURSTOMASZA@GMAIL.COM" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "1a4a698c-13c7-49fe-800d-5c6dd24d15ec", "AQAAAAIAAYagAAAAEOq7Oz9CZyoxQxlsQ/hEM51V5DsIvWTud8eZuWwIpAzkMsUEJnSfmP865ZIQZ+bLMw==", "c46a3e71-a556-4eb4-9354-b32c7f3507b8", "Tomasz Tomaszewicz" });
        }
    }
}
