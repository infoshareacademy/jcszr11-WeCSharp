using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityFixing3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d3f2e54-3d40-4823-9938-2e339a9b96b9", null, "Admin", "ADMIN" },
                    { "dd96a05a-bdf2-4fd5-b0a3-befe647fdbc5", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e5dc4fa2-d5d4-48f0-9e57-14f9ec5a2cde", 0, "9a883d51-fe1d-4496-906c-c0c0c242ca32", 1, null, true, false, null, "Name", null, null, null, null, false, 2, "d65ce551-e447-4382-b54a-4bd703bcb219", "fff", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d3f2e54-3d40-4823-9938-2e339a9b96b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd96a05a-bdf2-4fd5-b0a3-befe647fdbc5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5dc4fa2-d5d4-48f0-9e57-14f9ec5a2cde");

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

            migrationBuilder.InsertData(
                table: "CalendarEvents",
                columns: new[] { "Id", "CalendarEventDate", "CalendarEventDescription", "CalendarEventEndTime", "CalendarEventName", "CalendarEventStartTime", "UserId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 10), "Ongoing maintenance tasks in the office", new TimeOnly(9, 30, 0), "Maintenance Work", new TimeOnly(8, 30, 0), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 2, new DateOnly(2024, 1, 11), "Scheduled office cleaning day", new TimeOnly(10, 30, 0), "Office Cleaning", new TimeOnly(9, 30, 0), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 3, new DateOnly(2024, 1, 11), "Time for a relaxed atmosphere!", new TimeOnly(11, 30, 0), "Pottering", new TimeOnly(10, 30, 0), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 4, new DateOnly(2024, 1, 11), "Team project meeting to discuss progress, challenges, and plans for project execution", new TimeOnly(12, 30, 0), "Project Meeting", new TimeOnly(11, 30, 0), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 5, new DateOnly(2024, 1, 12), "Strategic business meeting covering company development, market strategy, and key decisions", new TimeOnly(13, 30, 0), "Business Meeting", new TimeOnly(12, 30, 0), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 6, new DateOnly(2024, 1, 13), "Educational workshop aimed at enhancing employee's skills", new TimeOnly(14, 30, 0), "Training Workshop", new TimeOnly(13, 30, 0), "8e445865-a24d-4543-a6c6-9443d048cdb9" }
                });

            migrationBuilder.InsertData(
                table: "WorkModesToUsers",
                columns: new[] { "Id", "DateOfWorkMode", "UserId", "WorkModeId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 10), "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 2, new DateOnly(2024, 1, 10), "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 3, new DateOnly(2024, 1, 11), "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 4, new DateOnly(2024, 1, 12), "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 5, new DateOnly(2024, 1, 13), "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 },
                    { 6, new DateOnly(2024, 1, 14), "8e445865-a24d-4543-a6c6-9443d048cdb9", 1 }
                });
        }
    }
}
