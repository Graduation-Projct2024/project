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
    [Migration("20240628110838_edit feedback")]
    partial class editfeedback
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("startDate")
                        .HasColumnType("date");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("subAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("totalHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("subAdminId");

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

                    b.Property<bool>("isHidden")
                        .HasColumnType("bit");

                    b.Property<string>("linkUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
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

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SubAdminId");

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

                    b.HasIndex("InstructorId");

                    b.HasIndex("StudentId");

                    b.ToTable("feedbacks");
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

            modelBuilder.Entity("courseProject.core.Models.Student_Task_Submissions", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("courseProject.Core.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("skillDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("703616ae-7629-4ea1-859f-c679b7658024"),
                            IsVerified = true,
                            dateOfAdded = new DateTime(2024, 6, 28, 14, 8, 37, 95, DateTimeKind.Local).AddTicks(757),
                            email = "programming.academy24@gmail.com",
                            password = "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse",
                            role = "admin",
                            userName = "admin"
                        });
                });

            modelBuilder.Entity("courseProject.Core.Models.Consultation", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "instructor")
                        .WithMany("consultations")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.User", "student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");

                    b.Navigation("student");
                });

            modelBuilder.Entity("courseProject.Core.Models.Course", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "instructor")
                        .WithMany("courses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.User", "subAdmin")
                        .WithMany()
                        .HasForeignKey("subAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");

                    b.Navigation("subAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.CourseMaterial", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "instructor")
                        .WithMany()
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

                    b.Navigation("consultation");

                    b.Navigation("instructor");
                });

            modelBuilder.Entity("courseProject.Core.Models.Event", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "subAdmin")
                        .WithMany("events")
                        .HasForeignKey("SubAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("subAdmin");
                });

            modelBuilder.Entity("courseProject.Core.Models.Feedback", b =>
                {
                    b.HasOne("courseProject.Core.Models.Course", "course")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CourseId");

                    b.HasOne("courseProject.Core.Models.User", "instructor")
                        .WithMany("feedbacks")
                        .HasForeignKey("InstructorId");

                    b.HasOne("courseProject.Core.Models.User", "student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("instructor");

                    b.Navigation("student");
                });

            modelBuilder.Entity("courseProject.Core.Models.Instructor_Working_Hours", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");
                });

            modelBuilder.Entity("courseProject.Core.Models.InstructorSkills", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "instructor")
                        .WithMany("instructorSkills")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("courseProject.Core.Models.Skills", "Skills")
                        .WithMany("instructorSkills")
                        .HasForeignKey("skillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skills");

                    b.Navigation("instructor");
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
                    b.HasOne("courseProject.Core.Models.User", "student")
                        .WithMany("requests")
                        .HasForeignKey("StudentId");

                    b.Navigation("student");
                });

            modelBuilder.Entity("courseProject.core.Models.Student_Task_Submissions", b =>
                {
                    b.HasOne("courseProject.Core.Models.User", "Student")
                        .WithMany()
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
                    b.HasOne("courseProject.Core.Models.User", "Student")
                        .WithMany()
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
                    b.HasOne("courseProject.Core.Models.User", "Student")
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

            modelBuilder.Entity("courseProject.Core.Models.Skills", b =>
                {
                    b.Navigation("instructorSkills");
                });

            modelBuilder.Entity("courseProject.Core.Models.User", b =>
                {
                    b.Navigation("consultations");

                    b.Navigation("courses");

                    b.Navigation("events");

                    b.Navigation("feedbacks");

                    b.Navigation("instructorSkills");

                    b.Navigation("requests");

                    b.Navigation("studentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
