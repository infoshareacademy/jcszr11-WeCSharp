using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Migrations
{
    public partial class additionalmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", },
                values: new object[] { "97f9c9ce-6f3b-4231-a3da-ffdb9e340ce8", "AQAAAAIAAYagAAAAEMkp6ZgI7B0CvWNq7cpXxQLfp7JOOxqYJlMMp1bU4SUbCSSGgZScHqnwn+DCs48pvg==", "518336dd-50dc-463c-893c-b5f630eba94b", "kurstomasza@gmail.com","KURSTOMASZA@GMAIL.COM", "kurstomasza@gmail.com", "KURSTOMASZA@GMAIL.COM" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", },
                values: new object[] { "e03d22d8-92e6-4014-95f4-f94624339ecf", "AQAAAAIAAYagAAAAEMkp6ZgI7B0CvWNq7cpXxQLfp7JOOxqYJlMMp1bU4SUbCSSGgZScHqnwn+DCs48pvg==", "68070611-8b1a-4521-9b41-29209ea21f6f", "kursandrzeja@gmail.com", "KURSANDRZEJA@GMAIL.COM", "kursandrzeja@gmail.com", "KURSANDRZEJA@GMAIL.COM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "NormalizedEmail" },
                values: new object[] { "b671d221-ee51-4eee-8d1b-ce35440e9b5d", "AQAAAAIAAYagAAAAEOtbN3nlp4V8e1cIpZ847YebM0SOh54N7KgdROwl4ju3WO0hk60GrXkNjg8ERc7Y4w==", "b72bb8c9-05ce-4b6e-8495-dc8dbeb39f17", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "NormalizedEmail" },
                values: new object[] { "6a51b9ba-147c-4d6c-9232-a92f8dd4a43c", "AQAAAAIAAYagAAAAEOtbN3nlp4V8e1cIpZ847YebM0SOh54N7KgdROwl4ju3WO0hk60GrXkNjg8ERc7Y4w==", "1b612341-087e-44f2-8ded-ff50bafc3966", null });
        }
    }
}
