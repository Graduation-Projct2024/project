using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addDateTimeToStudentSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("27112ae2-c1e6-436c-bbb0-338d7345a9d0"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("27112ae2-c1e6-436c-bbb0-338d7345a9d0"));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateOfAdded",
                table: "Student_Task_Submissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "email", "gender", "password", "phoneNumber", "role", "userName" },
                values: new object[] { new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"), null, null, true, null, null, "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                column: "AdminId",
                value: new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"));

            migrationBuilder.DropColumn(
                name: "dateOfAdded",
                table: "Student_Task_Submissions");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "email", "gender", "password", "phoneNumber", "role", "userName" },
                values: new object[] { new Guid("27112ae2-c1e6-436c-bbb0-338d7345a9d0"), null, null, true, null, null, "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                column: "AdminId",
                value: new Guid("27112ae2-c1e6-436c-bbb0-338d7345a9d0"));
        }
    }
}
