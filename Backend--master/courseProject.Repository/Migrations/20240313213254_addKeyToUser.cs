using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addKeyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_General_Feedback_users_email",
                table: "General_Feedback");

            migrationBuilder.DropIndex(
                name: "IX_General_Feedback_email",
                table: "General_Feedback");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "General_Feedback",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Useremail",
                table: "General_Feedback",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_General_Feedback_Useremail",
                table: "General_Feedback",
                column: "Useremail");

            migrationBuilder.AddForeignKey(
                name: "FK_General_Feedback_users_Useremail",
                table: "General_Feedback",
                column: "Useremail",
                principalTable: "users",
                principalColumn: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_General_Feedback_users_Useremail",
                table: "General_Feedback");

            migrationBuilder.DropIndex(
                name: "IX_General_Feedback_Useremail",
                table: "General_Feedback");

            migrationBuilder.DropColumn(
                name: "Useremail",
                table: "General_Feedback");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "General_Feedback",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_General_Feedback_email",
                table: "General_Feedback",
                column: "email");

            migrationBuilder.AddForeignKey(
                name: "FK_General_Feedback_users_email",
                table: "General_Feedback",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
