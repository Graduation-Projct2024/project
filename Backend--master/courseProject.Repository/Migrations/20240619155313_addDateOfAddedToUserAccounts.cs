using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addDateOfAddedToUserAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateOfAdded",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "dateOfAdded", "email", "gender", "password", "phoneNumber", "role", "userName" },
                values: new object[] { new Guid("6b8929ec-a3be-4431-abca-a7603b0a09b7"), null, null, true, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                column: "AdminId",
                value: new Guid("6b8929ec-a3be-4431-abca-a7603b0a09b7"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("6b8929ec-a3be-4431-abca-a7603b0a09b7"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("6b8929ec-a3be-4431-abca-a7603b0a09b7"));

            migrationBuilder.DropColumn(
                name: "dateOfAdded",
                table: "users");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "DateOfBirth", "ImageUrl", "IsVerified", "LName", "address", "email", "gender", "password", "phoneNumber", "role", "userName" },
                values: new object[] { new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"), null, null, true, null, null, "programming.academy24@gmail.com", null, "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                column: "AdminId",
                value: new Guid("59b5d7c2-bee9-4763-8b0c-f0d762df8e90"));
        }
    }
}
