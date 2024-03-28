using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7ce920fe-0847-4246-b7d2-44f985695fb6", "c5151d4f-1c9b-46ba-afd3-ff6231d38880" });
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
