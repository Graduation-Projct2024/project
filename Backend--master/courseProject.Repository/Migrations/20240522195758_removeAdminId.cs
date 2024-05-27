using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class removeAdminId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_requests_admins_AdminId",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_requests_AdminId",
                table: "requests");

            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("7a99c882-9526-4de3-95f1-5acb435152e2"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("7a99c882-9526-4de3-95f1-5acb435152e2"));

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "requests");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"), false, "manar0fuqha2002189@gmail.com", "$2a$11$EmAYVIpLbbNEepKvCZovpODYakSaOGpTh9e7Osw2MdvDzEpjl.Y0W", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"), null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "requests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("7a99c882-9526-4de3-95f1-5acb435152e2"), false, "manar0fuqha2002189@gmail.com", "$2a$11$EmAYVIpLbbNEepKvCZovpODYakSaOGpTh9e7Osw2MdvDzEpjl.Y0W", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("7a99c882-9526-4de3-95f1-5acb435152e2"), null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_requests_AdminId",
                table: "requests",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_requests_admins_AdminId",
                table: "requests",
                column: "AdminId",
                principalTable: "admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
