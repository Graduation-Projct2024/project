using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addIsHidden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("628377b6-06c0-44ac-bc6c-aa54e93e47c6"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("628377b6-06c0-44ac-bc6c-aa54e93e47c6"));

            migrationBuilder.AddColumn<bool>(
                name: "isHidden",
                table: "courseMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("5807c1d6-369f-4141-8450-9bdcbd5ac91f"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("5807c1d6-369f-4141-8450-9bdcbd5ac91f"), null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("5807c1d6-369f-4141-8450-9bdcbd5ac91f"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("5807c1d6-369f-4141-8450-9bdcbd5ac91f"));

            migrationBuilder.DropColumn(
                name: "isHidden",
                table: "courseMaterials");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("628377b6-06c0-44ac-bc6c-aa54e93e47c6"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("628377b6-06c0-44ac-bc6c-aa54e93e47c6"), null, null, null, null, null, null });
        }
    }
}
