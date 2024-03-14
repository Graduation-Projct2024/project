using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class editKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admins_users_email",
                table: "admins");

            migrationBuilder.DropForeignKey(
                name: "FK_General_Feedback_users_Useremail",
                table: "General_Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_users_email",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_students_users_email",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_subadmins_users_email",
                table: "subadmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_subadmins_email",
                table: "subadmins");

            migrationBuilder.DropIndex(
                name: "IX_students_email",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_instructors_email",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_General_Feedback_Useremail",
                table: "General_Feedback");

            migrationBuilder.DropIndex(
                name: "IX_admins_email",
                table: "admins");

            migrationBuilder.DropColumn(
                name: "Useremail",
                table: "General_Feedback");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "subadmins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "General_Feedback",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "admins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");

         

            

           

           

            //migrationBuilder.AddForeignKey(
            //    name: "FK_admins_users_AId",
            //    table: "admins",
            //    column: "AId",
            //    principalTable: "users",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_General_Feedback_users_UserId",
            //    table: "General_Feedback",
            //    column: "UserId",
            //    principalTable: "users",
            //    principalColumn: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_instructors_users_IId",
            //    table: "instructors",
            //    column: "IId",
            //    principalTable: "users",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_students_users_SId",
            //    table: "students",
            //    column: "SId",
            //    principalTable: "users",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_subadmins_users_SAId",
            //    table: "subadmins",
            //    column: "SAId",
            //    principalTable: "users",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admins_users_AId",
                table: "admins");

            migrationBuilder.DropForeignKey(
                name: "FK_General_Feedback_users_UserId",
                table: "General_Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_users_IId",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_students_users_SId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_subadmins_users_SAId",
                table: "subadmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

           

            

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "General_Feedback");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "subadmins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "instructors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Useremail",
                table: "General_Feedback",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "admins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_subadmins_email",
                table: "subadmins",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_email",
                table: "students",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_instructors_email",
                table: "instructors",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_General_Feedback_Useremail",
                table: "General_Feedback",
                column: "Useremail");

            migrationBuilder.CreateIndex(
                name: "IX_admins_email",
                table: "admins",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_admins_users_email",
                table: "admins",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_General_Feedback_users_Useremail",
                table: "General_Feedback",
                column: "Useremail",
                principalTable: "users",
                principalColumn: "email");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_users_email",
                table: "instructors",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_users_email",
                table: "students",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subadmins_users_email",
                table: "subadmins",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
