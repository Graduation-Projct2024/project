using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courseProject.Repository.Migrations
{
    public partial class addTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admins_users_email",
                        column: x => x.email,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "General_Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_General_Feedback_users_email",
                        column: x => x.email,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_instructors_users_email",
                        column: x => x.email,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_users_email",
                        column: x => x.email,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subadmins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subadmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subadmins_users_email",
                        column: x => x.email,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_Working_Hours",
                columns: table => new
                {
                    instructorId = table.Column<int>(type: "int", nullable: false),
                    day = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_Working_Hours", x => new { x.instructorId, x.day, x.startTime, x.endTime });
                    table.ForeignKey(
                        name: "FK_Instructor_Working_Hours_instructors_instructorId",
                        column: x => x.instructorId,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    instructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultation_instructors_instructorId",
                        column: x => x.instructorId,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultation_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    instructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructor_Feedback_instructors_instructorId",
                        column: x => x.instructorId,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructor_Feedback_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    satus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    adminId = table.Column<int>(type: "int", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    subAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requests_admins_adminId",
                        column: x => x.adminId,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_requests_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_requests_subadmins_subAdminId",
                        column: x => x.subAdminId,
                        principalTable: "subadmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    instructorId = table.Column<int>(type: "int", nullable: false),
                    subAdminId = table.Column<int>(type: "int", nullable: false),
                    requestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courses_instructors_instructorId",
                        column: x => x.instructorId,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courses_requests_requestId",
                        column: x => x.requestId,
                        principalTable: "requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_courses_subadmins_subAdminId",
                        column: x => x.subAdminId,
                        principalTable: "subadmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subAdminId = table.Column<int>(type: "int", nullable: false),
                    requestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_requests_requestId",
                        column: x => x.requestId,
                        principalTable: "requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_subadmins_subAdminId",
                        column: x => x.subAdminId,
                        principalTable: "subadmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Course_Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Feedback_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Feedback_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "courseMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pdfUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    instructorId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courseMaterials_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_courseMaterials_instructors_instructorId",
                        column: x => x.instructorId,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    studentId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.studentId, x.courseId });
                    table.ForeignKey(
                        name: "FK_StudentCourse_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_email",
                table: "admins",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_instructorId",
                table: "Consultation",
                column: "instructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_studentId",
                table: "Consultation",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Feedback_courseId",
                table: "Course_Feedback",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Feedback_studentId",
                table: "Course_Feedback",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_courseMaterials_courseId",
                table: "courseMaterials",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_courseMaterials_instructorId",
                table: "courseMaterials",
                column: "instructorId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_instructorId",
                table: "courses",
                column: "instructorId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_requestId",
                table: "courses",
                column: "requestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_subAdminId",
                table: "courses",
                column: "subAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_events_requestId",
                table: "events",
                column: "requestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_subAdminId",
                table: "events",
                column: "subAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_General_Feedback_email",
                table: "General_Feedback",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_Feedback_instructorId",
                table: "Instructor_Feedback",
                column: "instructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_Feedback_studentId",
                table: "Instructor_Feedback",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_email",
                table: "instructors",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requests_adminId",
                table: "requests",
                column: "adminId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_studentId",
                table: "requests",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_subAdminId",
                table: "requests",
                column: "subAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_courseId",
                table: "StudentCourse",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_students_email",
                table: "students",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subadmins_email",
                table: "subadmins",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultation");

            migrationBuilder.DropTable(
                name: "Course_Feedback");

            migrationBuilder.DropTable(
                name: "courseMaterials");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "General_Feedback");

            migrationBuilder.DropTable(
                name: "Instructor_Feedback");

            migrationBuilder.DropTable(
                name: "Instructor_Working_Hours");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "instructors");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subadmins");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
