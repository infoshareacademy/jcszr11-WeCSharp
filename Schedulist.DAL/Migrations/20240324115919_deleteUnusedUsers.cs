using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteUnusedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");         

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "04c47714-6d4f-43dc-a88b-573a6fb80f3a", "0bbeaf39-9b10-433c-80af-fa09fd3d2488" });

            migrationBuilder.UpdateData(
                table: "WorkModes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Home office");

            migrationBuilder.UpdateData(
                table: "WorkModes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sick leave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8988da4d-4f4f-425d-aecf-b1244e1ceaf1", "23774e50-a40c-4fc2-94da-97e06a76a0ac" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2", 50, "df6c20b3-31fd-4ddc-bc40-5cd6f6638e21", 3, null, true, false, null, "Andrzej", null, null, null, null, false, 4, "e98c1f5b-ba90-4b60-9fd2-75d68cec59f6", "Andrzejewicz", false, "Andrzej Andrzejewicz" },
                    { "3", 50, "d7328ece-c1eb-4470-937c-81fca5e22d17", 3, null, true, false, null, "Michael", null, null, null, null, false, 1, "aa142be7-8373-4a4b-90b2-71ed464b9476", "Jordan", false, "kursmichaela@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "WorkModes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "HomeOffice");

            migrationBuilder.UpdateData(
                table: "WorkModes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "SickLeave");
        }
    }
}
