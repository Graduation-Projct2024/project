using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class editusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "subadmins");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "subadmins");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "subadmins");

            migrationBuilder.DropColumn(
                name: "address",
                table: "subadmins");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "subadmins");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "subadmins");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "students");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "students");

            migrationBuilder.DropColumn(
                name: "address",
                table: "students");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "students");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "address",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "admins");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "admins");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "admins");

            migrationBuilder.DropColumn(
                name: "address",
                table: "admins");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "admins");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "admins");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "email", "gender", "password", "phoneNumber", "role", "userName" },
                values: new object[] { new Guid("d4b70244-1065-46ca-abe8-d11ed32fe38f"), null, null, true, null, null, "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                column: "AdminId",
                value: new Guid("d4b70244-1065-46ca-abe8-d11ed32fe38f"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("d4b70244-1065-46ca-abe8-d11ed32fe38f"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("d4b70244-1065-46ca-abe8-d11ed32fe38f"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address",
                table: "users");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "users");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "subadmins",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "subadmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "subadmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "subadmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "subadmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "subadmins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "students",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "instructors",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "admins",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"), null, null, null, null, null, null });
        }
    }
}
