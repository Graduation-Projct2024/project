using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class editinCourseMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseMaterials_courses_courseId",
                table: "courseMaterials");

            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("23cab1b4-2f7a-49a8-a4fb-15d54f96ac88"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("23cab1b4-2f7a-49a8-a4fb-15d54f96ac88"));

            migrationBuilder.AlterColumn<Guid>(
                name: "courseId",
                table: "courseMaterials",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("c1f80478-28de-41c7-bce3-1c156b59f76d"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("c1f80478-28de-41c7-bce3-1c156b59f76d"), null, null, null, null, null, null });

            migrationBuilder.AddForeignKey(
                name: "FK_courseMaterials_courses_courseId",
                table: "courseMaterials",
                column: "courseId",
                principalTable: "courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseMaterials_courses_courseId",
                table: "courseMaterials");

            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: new Guid("c1f80478-28de-41c7-bce3-1c156b59f76d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("c1f80478-28de-41c7-bce3-1c156b59f76d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "courseId",
                table: "courseMaterials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "IsVerified", "email", "password", "role", "userName" },
                values: new object[] { new Guid("23cab1b4-2f7a-49a8-a4fb-15d54f96ac88"), true, "programming.academy24@gmail.com", "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "DateOfBirth", "ImageUrl", "LName", "address", "gender", "phoneNumber" },
                values: new object[] { new Guid("23cab1b4-2f7a-49a8-a4fb-15d54f96ac88"), null, null, null, null, null, null });

            migrationBuilder.AddForeignKey(
                name: "FK_courseMaterials_courses_courseId",
                table: "courseMaterials",
                column: "courseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
