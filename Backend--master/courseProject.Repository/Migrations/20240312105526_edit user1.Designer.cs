﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using courseProject.Repository.Data;

#nullable disable

namespace courseProject.Repository.Migrations
{
    [DbContext(typeof(projectDbContext))]
    [Migration("20240312105526_edit user1")]
    partial class edituser1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("courseProject.Core.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("admins");
                });

            modelBuilder.Entity("courseProject.Core.Models.Consultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("instructorId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("instructorId");

                    b.HasIndex("studentId");

                    b.ToTable("Consultation");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("instructorId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<int>("requestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("subAdminId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("instructorId");

                    b.HasIndex("requestId")
                        .IsUnique();

                    b.HasIndex("subAdminId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course_Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("courseId");

                    b.HasIndex("studentId");

                    b.ToTable("Course_Feedback");
                });

            modelBuilder.Entity("courseProject.Core.Models.CourseMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("instructorId")
                        .HasColumnType("int");

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

                    b.HasIndex("courseId");

                    b.HasIndex("instructorId");

                    b.ToTable("courseMaterials");
                });

            modelBuilder.Entity("courseProject.Core.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateOfEvent")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("requestId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("subAdminId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("requestId")
                        .IsUnique();

                    b.HasIndex("subAdminId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("courseProject.Core.Models.General_Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("email");

                    b.ToTable("General_Feedback");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("instructors");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("instructorId")
                        .HasColumnType("int");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("instructorId");

                    b.HasIndex("studentId");

                    b.ToTable("Instructor_Feedback");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Working_Hours", b =>
                {
                    b.Property<int>("instructorId")
                        .HasColumnType("int");

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime2");

                    b.HasKey("instructorId", "day", "startTime", "endTime");

                    b.ToTable("Instructor_Working_Hours");
                });

            modelBuilder.Entity("courseProject.Core.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("adminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("satus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("studentId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("subAdminId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("adminId");

                    b.HasIndex("studentId");

                    b.HasIndex("subAdminId");

                    b.ToTable("requests");
                });

            modelBuilder.Entity("courseProject.Core.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("students");
                });

            modelBuilder.Entity("courseProject.Core.Models.StudentCourse", b =>
                {
                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollDate")
                        .HasColumnType("datetime2");

                    b.HasKey("studentId", "courseId");

                    b.HasIndex("courseId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("courseProject.Core.Models.SubAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("subadmins");
                });

            modelBuilder.Entity("courseProject.Core.Models.User", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("users");
                });

            modelBuilder.Entity("courseProject.Core.Models.Admin", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("admin")
                        .HasForeignKey("courseProject.Core.Models.Admin", "email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.Consultation", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "instructor")
                        .WithMany("Consultations")
                        .HasForeignKey("instructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Student", "student")
                        .WithMany("Consultations")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");

                    b.Navigation("student");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("instructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Request", "Request")
                        .WithOne("Course")
                        .HasForeignKey("courseProject.Core.Models.Course", "requestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.SubAdmin", "SubAdmin")
                        .WithMany()
                        .HasForeignKey("subAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Request");

                    b.Navigation("SubAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course_Feedback", b =>
                {
                    b.HasOne("courseProject.Core.Models.Course", "course")
                        .WithMany("course_Feedbacks")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("course_Feedbacks")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("course");
                });

            modelBuilder.Entity("courseProject.Core.Models.CourseMaterial", b =>
                {
                    b.HasOne("courseProject.Core.Models.Course", "Course")
                        .WithMany("Materials")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Instructor", "Instructor")
                        .WithMany("Materials")
                        .HasForeignKey("instructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("courseProject.Core.Models.Event", b =>
                {
                    b.HasOne("courseProject.Core.Models.Request", "Request")
                        .WithOne("Event")
                        .HasForeignKey("courseProject.Core.Models.Event", "requestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.SubAdmin", "SubAdmin")
                        .WithMany("events")
                        .HasForeignKey("subAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("SubAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.General_Feedback", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "User")
                        .WithMany("general_feedback")
                        .HasForeignKey("email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("instructor")
                        .HasForeignKey("courseProject.Core.Models.Instructor", "email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Feedback", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "Instructor")
                        .WithMany("instructor_Feedbacks")
                        .HasForeignKey("instructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("instructor_Feedbacks")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Working_Hours", b =>
                {
                    b.HasOne("courseProject.Core.Models.Instructor", "instructor")
                        .WithMany("Instructor_Working_Hours")
                        .HasForeignKey("instructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");
                });

            modelBuilder.Entity("courseProject.Core.Models.Request", b =>
                {
                    b.HasOne("courseProject.Core.Models.Admin", "Admin")
                        .WithMany("requests")
                        .HasForeignKey("adminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("requests")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.SubAdmin", "SubAdmin")
                        .WithMany("requests")
                        .HasForeignKey("subAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Student");

                    b.Navigation("SubAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.Student", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("student")
                        .HasForeignKey("courseProject.Core.Models.Student", "email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.StudentCourse", b =>
                {
                    b.HasOne("courseProject.Core.Models.Course", "Course")
                        .WithMany("studentCourses")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Student", "Student")
                        .WithMany("studentCourses")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("courseProject.Core.Models.SubAdmin", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "user")
                        .WithOne("subadmin")
                        .HasForeignKey("courseProject.Core.Models.SubAdmin", "email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("courseProject.Core.Models.Admin", b =>
                {
                    b.Navigation("requests");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("course_Feedbacks");

                    b.Navigation("studentCourses");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Instructor_Working_Hours");

                    b.Navigation("Materials");

                    b.Navigation("instructor_Feedbacks");
                });

            modelBuilder.Entity("courseProject.Core.Models.Request", b =>
                {
                    b.Navigation("Course")
                        .IsRequired();

                    b.Navigation("Event")
                        .IsRequired();
                });

            modelBuilder.Entity("courseProject.Core.Models.Student", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("course_Feedbacks");

                    b.Navigation("instructor_Feedbacks");

                    b.Navigation("requests");

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

                    b.Navigation("general_feedback");

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
