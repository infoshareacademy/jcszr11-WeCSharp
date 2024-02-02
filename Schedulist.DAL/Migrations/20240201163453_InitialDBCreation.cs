using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication4.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPrivilege = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    CalendarEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarEventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalendarEventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalendarEventDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CalendarEventStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CalendarEventEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    AssignedToUser = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.CalendarEventId);
                    table.ForeignKey(
                        name: "FK_CalendarEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkModesToUsers",
                columns: table => new
                {
                    WorkModeToUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkModeId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DateOfWorkMode = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkModesToUsers", x => x.WorkModeToUserID);
                    table.ForeignKey(
                        name: "FK_WorkModesToUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkModesToUsers_WorkModes_WorkModeId",
                        column: x => x.WorkModeId,
                        principalTable: "WorkModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkModes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Office" },
                    { 2, "HomeOffice" },
                    { 3, "SickLeave" },
                    { 4, "Delegation" },
                    { 5, "Holiday" },
                    { 6, "Others" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_UserId",
                table: "CalendarEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkModesToUsers_UserID",
                table: "WorkModesToUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkModesToUsers_WorkModeId",
                table: "WorkModesToUsers",
                column: "WorkModeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "WorkModesToUsers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkModes");
        }
    }
}
