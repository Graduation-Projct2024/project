using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class removePdfUrlFromMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "pdfUrl",
                table: "courseMaterials");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"), null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("1adead10-ed9e-45a4-98d5-d0a209b48254"));

            migrationBuilder.AddColumn<string>(
                name: "pdfUrl",
                table: "courseMaterials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("5807c1d6-369f-4141-8450-9bdcbd5ac91f"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("5807c1d6-369f-4141-8450-9bdcbd5ac91f"), null, null, null, null, null, null });
        }
    }
}
