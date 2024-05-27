using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("e132c30a-3d4e-40c7-94d4-873d25afc56e"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("e132c30a-3d4e-40c7-94d4-873d25afc56e"), null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("e132c30a-3d4e-40c7-94d4-873d25afc56e"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("e132c30a-3d4e-40c7-94d4-873d25afc56e"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"), false, "manar0fuqha2002189@gmail.com", "$2a$11$EmAYVIpLbbNEepKvCZovpODYakSaOGpTh9e7Osw2MdvDzEpjl.Y0W", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("d6f83557-8a8f-4cc8-847a-559035bb4401"), null, null, null, null, null, null });
        }
    }
}
