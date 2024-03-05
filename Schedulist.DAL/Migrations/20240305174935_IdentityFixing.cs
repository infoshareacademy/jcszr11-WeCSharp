using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedulist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_Users_UserId",
                table: "CalendarEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkModesToUsers_Users_UserId",
                table: "WorkModesToUsers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c7a9335-c7ac-4161-b5ad-ccf6cddea0c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cd09253-779b-4ac5-ad06-cba3950d1dc5");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkModesToUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CalendarEvents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_AspNetUsers_UserId",
                table: "CalendarEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkModesToUsers_AspNetUsers_UserId",
                table: "WorkModesToUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_AspNetUsers_UserId",
                table: "CalendarEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkModesToUsers_AspNetUsers_UserId",
                table: "WorkModesToUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "503f02fc-28c1-45b5-843b-4906e034f1cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bc90f65-ff56-45e6-a77b-f4de8fd9fbd1");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WorkModesToUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CalendarEvents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    AdminPrivilege = table.Column<bool>(type: "bit", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c7a9335-c7ac-4161-b5ad-ccf6cddea0c7", null, "User", "USER" },
                    { "3cd09253-779b-4ac5-ad06-cba3950d1dc5", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CalendarEvents",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AdminPrivilege", "DepartmentId", "Login", "Name", "Password", "PositionId", "Surname" },
                values: new object[,]
                {
                    { 1, false, 1, "Log1", "Tomasz", "Pass1", 1, "Tomaszewicz" },
                    { 2, false, 1, "Log2", "Andrzej", "Pass2", 1, "Andrzejewski" },
                    { 3, false, 1, "Log3", "Romek", "Pass2", 1, "Romanowicz" },
                    { 4, true, 1, "Log4", "Zbigniew", "Pass3", 1, "Zero" },
                    { 5, false, 1, "Log5", "Jordan", "Pass4", 1, "Michael" },
                    { 6, false, 1, "Log6", "Marta", "Pass5", 1, "Debowska" }
                });

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WorkModesToUsers",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_Users_UserId",
                table: "CalendarEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkModesToUsers_Users_UserId",
                table: "WorkModesToUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
