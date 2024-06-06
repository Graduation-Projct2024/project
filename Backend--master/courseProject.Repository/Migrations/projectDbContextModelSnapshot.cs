﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using courseProject.Repository.Data;

#nullable disable

namespace courseProject.Repository.Migrations
{
    [DbContext(typeof(projectDbContext))]
    partial class projectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("courseProject.Core.Models.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("admins");

                    b.HasData(
                        new
                        {
                            AdminId = new Guid("628377b6-06c0-44ac-bc6c-aa54e93e47c6")
                        });
                });

            modelBuilder.Entity("courseProject.Core.Models.Consultation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("date")
                        .HasColumnType("date");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("endTime")
                        .HasColumnType("time");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("startTime")
                        .HasColumnType("time");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("StudentId");

                    b.ToTable("consultations");
                });

            modelBuilder.Entity("courseProject.Core.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("contacts");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateOfUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("endDate")
                        .HasColumnType("date");

                    b.Property<int?>("limitNumberOfStudnet")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<Guid>("requestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("startDate")
                        .HasColumnType("date");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("totalHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("SubAdminId");

                    b.HasIndex("requestId")
                        .IsUnique();

                    b.ToTable("courses");
                });

            modelBuilder.Entity("courseProject.Core.Models.CourseMaterial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("consultationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("courseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("linkUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("consultationId");

                    b.HasIndex("courseId");

                    b.ToTable("courseMaterials");
                });

            modelBuilder.Entity("courseProject.Core.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SubAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateOfEvent")
                        .HasColumnType("date");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("requestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SubAdminId");

                    b.HasIndex("requestId")
                        .IsUnique();

                    b.ToTable("events");
                });

            modelBuilder.Entity("courseProject.Core.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<int?>("range")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("feedbacks");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor", b =>
                {
                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("skillDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructorId");

                    b.ToTable("instructors");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Working_Hours", b =>
                {
                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("startTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("endTime")
                        .HasColumnType("time");

                    b.HasKey("InstructorId", "day", "startTime", "endTime");

                    b.ToTable("Instructor_Working_Hours");
                });

            modelBuilder.Entity("courseProject.Core.Models.InstructorSkills", b =>
                {
                    b.Property<Guid>("skillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("skillId", "InstructorId");

                    b.HasIndex("InstructorId");

                    b.ToTable("InstructorSkills");
                });

            modelBuilder.Entity("courseProject.Core.Models.MaterialFiles", b =>
                {
                    b.Property<Guid>("materialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("pdfUrl")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("materialId", "pdfUrl");

                    b.ToTable("MaterialFiles");
                });

            modelBuilder.Entity("courseProject.Core.Models.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("endDate")
                        .HasColumnType("date");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("satus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("startDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubAdminId");

                    b.ToTable("requests");
                });

            modelBuilder.Entity("courseProject.Core.Models.Skills", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("courseProject.Core.Models.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("courseProject.core.Models.Student_Task_Submissions", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("Student_Task_Submissions");
                });

            modelBuilder.Entity("courseProject.Core.Models.StudentConsultations", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("consultationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EnrollDate")
                        .HasColumnType("datetime2");

                    b.HasKey("StudentId", "consultationId");

                    b.HasIndex("consultationId");

                    b.ToTable("StudentConsultations");
                });

            modelBuilder.Entity("courseProject.Core.Models.StudentCourse", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("courseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EnrollDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId", "courseId");

                    b.HasIndex("courseId");

                    b.ToTable("studentCourses");
                });

            modelBuilder.Entity("courseProject.Core.Models.SubAdmin", b =>
                {
                    b.Property<Guid>("SubAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubAdminId");

                    b.ToTable("subadmins");
                });

            modelBuilder.Entity("courseProject.Core.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("628377b6-06c0-44ac-bc6c-aa54e93e47c6"),
                            IsVerified = true,
                            email = "programming.academy24@gmail.com",
                            password = "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse",
                            role = "admin",
                            userName = "admin"
                        });
                });

            modelBuilder.Entity("courseProject.Core.Models.Admin", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("admin")
                        .HasForeignKey("courseProject.Core.Models.Admin", "AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.Consultation", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "instructor")
                        .WithMany("Consultations")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Student", "student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");

                    b.Navigation("student");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.SubAdmin", "SubAdmin")
                        .WithMany()
                        .HasForeignKey("SubAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Request", "Request")
                        .WithOne("Course")
                        .HasForeignKey("courseProject.Core.Models.Course", "requestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Request");

                    b.Navigation("SubAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.CourseMaterial", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "Instructor")
                        .WithMany("Materials")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Consultation", "consultation")
                        .WithMany()
                        .HasForeignKey("consultationId");

                    b.HasOne("courseProject.Core.Models.Course", "Course")
                        .WithMany("Materials")
                        .HasForeignKey("courseId");

                    b.Navigation("Course");

                    b.Navigation("Instructor");

                    b.Navigation("consultation");
                });

            modelBuilder.Entity("courseProject.Core.Models.Event", b =>
                {
                    b.HasOne("courseProject.Core.Models.SubAdmin", "SubAdmin")
                        .WithMany("events")
                        .HasForeignKey("SubAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Request", "Request")
                        .WithOne("Event")
                        .HasForeignKey("courseProject.Core.Models.Event", "requestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("SubAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.Feedback", b =>
                {
                    b.HasOne("courseProject.Core.Models.Course", "course")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CourseId");

                    b.HasOne("courseProject.Core.Models.User", "User")
                        .WithMany("feedbacks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("course");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("instructor")
                        .HasForeignKey("courseProject.Core.Models.Instructor", "InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Working_Hours", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "instructor")
                        .WithMany("Instructor_Working_Hours")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");
                });

            modelBuilder.Entity("courseProject.Core.Models.InstructorSkills", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "Instructor")
                        .WithMany("instructorSkills")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Skills", "Skills")
                        .WithMany("instructorSkills")
                        .HasForeignKey("skillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("courseProject.Core.Models.MaterialFiles", b =>
                {
                    b.HasOne("courseProject.Core.Models.CourseMaterial", "CourseMaterial")
                        .WithMany("MaterialFiles")
                        .HasForeignKey("materialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseMaterial");
                });

            modelBuilder.Entity("courseProject.Core.Models.Request", b =>
                {
                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("requests")
                        .HasForeignKey("StudentId");

                    b.HasOne("courseProject.Core.Models.SubAdmin", "SubAdmin")
                        .WithMany("requests")
                        .HasForeignKey("SubAdminId");

                    b.Navigation("Student");

                    b.Navigation("SubAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.Student", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("student")
                        .HasForeignKey("courseProject.Core.Models.Student", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.core.Models.Student_Task_Submissions", b =>
                {
                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("Student_Task_Submissions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.CourseMaterial", "CourseMaterial")
                        .WithMany("Student_Task_Submissions")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseMaterial");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("courseProject.Core.Models.StudentConsultations", b =>
                {
                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("studentConsultations")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Consultation", "consultation")
                        .WithMany("studentConsultations")
                        .HasForeignKey("consultationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("consultation");
                });

            modelBuilder.Entity("courseProject.Core.Models.StudentCourse", b =>
                {
                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("studentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Course", "Course")
                        .WithMany("studentCourses")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("courseProject.Core.Models.SubAdmin", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("subadmin")
                        .HasForeignKey("courseProject.Core.Models.SubAdmin", "SubAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.Consultation", b =>
                {
                    b.Navigation("studentConsultations");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Materials");

                    b.Navigation("studentCourses");
                });

            modelBuilder.Entity("courseProject.Core.Models.CourseMaterial", b =>
                {
                    b.Navigation("MaterialFiles");

                    b.Navigation("Student_Task_Submissions");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Instructor_Working_Hours");

                    b.Navigation("Materials");

                    b.Navigation("instructorSkills");
                });

            modelBuilder.Entity("courseProject.Core.Models.Request", b =>
                {
                    b.Navigation("Course")
                        .IsRequired();

                    b.Navigation("Event")
                        .IsRequired();
                });

            modelBuilder.Entity("courseProject.Core.Models.Skills", b =>
                {
                    b.Navigation("instructorSkills");
                });

            modelBuilder.Entity("courseProject.Core.Models.Student", b =>
                {
                    b.Navigation("Student_Task_Submissions");

                    b.Navigation("requests");

                    b.Navigation("studentConsultations");

                    b.Navigation("studentCourses");
                });

            modelBuilder.Entity("courseProject.Core.Models.SubAdmin", b =>
                {
                    b.Navigation("events");

                    b.Navigation("requests");
                });

            modelBuilder.Entity("courseProject.Core.Models.User", b =>
                {
                    b.Navigation("admin")
                        .IsRequired();

                    b.Navigation("feedbacks");

                    b.Navigation("instructor")
                        .IsRequired();

                    b.Navigation("student")
                        .IsRequired();

                    b.Navigation("subadmin")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
