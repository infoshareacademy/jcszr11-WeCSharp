using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserAdminUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23a83bb9-86f9-4bfc-bda8-0e23601e4f51", "AQAAAAIAAYagAAAAEA14Ca4Qxi1aLPhhX//QlyWzbRxLXzvnr+15a91K8Gm2wm4aJm8WmvYvHNbF5Uy/2Q==", "b047864c-3535-47b2-bb55-d8ba2ec4383c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f408323a-6f7b-4c2d-97ac-8a0911ac231c", "AQAAAAIAAYagAAAAEA14Ca4Qxi1aLPhhX//QlyWzbRxLXzvnr+15a91K8Gm2wm4aJm8WmvYvHNbF5Uy/2Q==", "a55540bb-c4ac-4d78-bfbb-d88cb0bffb11" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdd3807d-bd80-4b89-8019-bc586f7a200c", "AQAAAAIAAYagAAAAEOpf1xrTDc9k8UuPnFtamgzzhGaGs9U1C1VRNF5Ub100Qf1xgFUHFRWHkAp3YtEOng==", "c50db64e-f75d-4779-a596-4443ed581893" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3bd25510-972d-4f4c-a531-58daa32bfda2", "AQAAAAIAAYagAAAAEOpf1xrTDc9k8UuPnFtamgzzhGaGs9U1C1VRNF5Ub100Qf1xgFUHFRWHkAp3YtEOng==", "f1658d1e-712d-4971-913f-079f01e0b4f0" });
        }
    }
}
