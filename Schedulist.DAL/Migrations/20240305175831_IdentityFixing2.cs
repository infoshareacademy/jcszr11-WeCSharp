using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityFixing2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "503f02fc-28c1-45b5-843b-4906e034f1cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bc90f65-ff56-45e6-a77b-f4de8fd9fbd1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71ae69f6-4d0b-4d1a-a8a0-107c62b9b889", null, "Admin", "ADMIN" },
                    { "c88c23e1-b7ec-49c0-a72c-0f588f3c32c4", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "1dede95a-f722-49f3-a98b-66d8426bb27d", 1, null, true, false, null, "Name", null, null, null, null, false, 2, "28011aa0-7745-48a7-a38f-2c4085a89ec5", "fff", false, null });

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "8e445865-a24d-4543-a6c6-9443d048cdb9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71ae69f6-4d0b-4d1a-a8a0-107c62b9b889");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c88c23e1-b7ec-49c0-a72c-0f588f3c32c4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "503f02fc-28c1-45b5-843b-4906e034f1cd", null, "Admin", "ADMIN" },
                    { "9bc90f65-ff56-45e6-a77b-f4de8fd9fbd1", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "2");
        }
    }
}
