using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AllMigrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "70504250-bb0f-4522-8e92-c9a34d056d56", "f96c3099-d904-48d3-ae7a-165a5329bfcd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "73337519-3c97-45a0-907d-5e389e2823db", "a5e19c4b-af82-4d0d-ba7d-d26edae16001" });
        }
    }
}
