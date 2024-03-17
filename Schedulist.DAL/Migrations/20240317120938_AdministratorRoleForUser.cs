using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AdministratorRoleForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId"},
                values: new object[,]
                {
                    { "0ca508f0-94d9-484a-90d0-421d696e971e", "c3e92f9e-e8e9-4fe3-b600-ed1b055d25aa" },
                });
        }
      
    }
}
