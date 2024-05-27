using Microsoft.EntityFrameworkCore;
using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.core.Models;
using System.Security.Claims;

namespace courseProject.Repository.Data
{
    public class projectDbContext : DbContext
    {
        public projectDbContext() { }
        public projectDbContext(DbContextOptions<projectDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(
                         new User
                          {
                           UserId = userId,
                           userName = "admin",
                           email = "programming.academy24@gmail.com",
                           password = "$2a$11$cCCo7VY/4sxtIpeWDUFDROiDL/QPhi8AtawQhx4RGqKMAYgk8UIse",
                           role = "admin"
                           });

            modelBuilder.Entity<Admin>().HasData(
                          new Admin { AdminId = userId }
                         );
            modelBuilder.Entity<Instructor_Working_Hours>(
                e =>
                {
                    e.HasKey(x => new {x.InstructorId , x.day , x.startTime , x.endTime});
                   
                }
                );


            modelBuilder.Entity<User>()
       .HasMany(u => u.feedbacks)  // User has many Feedbacks
       .WithOne(f => f.User)       // Each Feedback belongs to one User
       .HasForeignKey(f => f.InstructorId);

            modelBuilder.Entity<User>()
        .HasMany(u => u.feedbacks)  // User has many Feedbacks
        .WithOne(f => f.User)       // Each Feedback belongs to one User
        .HasForeignKey(f => f.StudentId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.instructor)
                .WithOne(u => u.user)
                .HasForeignKey<Instructor>(x => x.InstructorId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.student)
                .WithOne(u => u.user)
                .HasForeignKey<Student>(x => x.StudentId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.admin)
                .WithOne(u => u.user)
                .HasForeignKey<Admin>(x => x.AdminId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.subadmin)
                .WithOne(u => u.user)
                .HasForeignKey<SubAdmin>(x => x.SubAdminId);



            modelBuilder.Entity<StudentCourse>(
                e =>
                {
                    e.HasKey(x=> new {x.StudentId , x.courseId});
                }
                );
            modelBuilder.Entity<StudentConsultations>(
               e =>
               {
                   e.HasKey(x => new { x.StudentId, x.consultationId });
               }
               );
            modelBuilder.Entity<Student_Task_Submissions>(
               e =>
               {
                   e.HasKey(x => new { x.StudentId, x.TaskId });
               }
               );
            modelBuilder.Entity<InstructorSkills>(
                e =>
                {
                    e.HasKey(x => new {  x.skillId , x.InstructorId });
                });
            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.StudentId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.SubAdminId)
                      .IsRequired(false);
            });

         

            modelBuilder.Entity<SubAdmin>()
           .Property(c => c.DateOfBirth)
           .HasColumnType("date");

            modelBuilder.Entity<Admin>()
           .Property(c => c.DateOfBirth)
           .HasColumnType("date");

            modelBuilder.Entity<Instructor>()
           .Property(c => c.DateOfBirth)
           .HasColumnType("date");

            modelBuilder.Entity<Student>()
           .Property(c => c.DateOfBirth)
           .HasColumnType("date");

            modelBuilder.Entity<Event>()
           .Property(c => c.dateOfEvent)
           .HasColumnType("date");

            modelBuilder.Entity<Course>()
           .Property(c => c.startDate)
           .HasColumnType("date");

            modelBuilder.Entity<Course>()
           .Property(c => c.endDate)
           .HasColumnType("date");

            modelBuilder.Entity<Course>()
           .Property(c => c.Deadline)
           .HasColumnType("date");

            modelBuilder.Entity<Consultation>()
           .Property(c => c.date)
           .HasColumnType("date");

            modelBuilder.Entity<Request>()
           .Property(c => c.startDate)
           .HasColumnType("date");

            modelBuilder.Entity<Request>()
           .Property(c => c.endDate)
           .HasColumnType("date");
            // modelBuilder.Entity<Student>()
            //.Property(s => s.Id)
            //.ValueGeneratedOnAdd()
            //.UseIdentityColumn();

        }

        public DbSet<User> users { get; set; }

        public DbSet<Instructor> instructors { get; set; }

        public DbSet<Student> students { get; set; }    

        public DbSet<Instructor_Working_Hours> Instructor_Working_Hours { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<SubAdmin> subadmins { get; set; }

        public DbSet<Request> requests { get; set; }

        public DbSet<Course> courses { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<CourseMaterial> courseMaterials { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        public DbSet<Consultation> consultations { get; set; }
       // public DbSet<Course_Feedback> course_Feedbacks { get; set; }
       // public DbSet<Instructor_Feedback> instructor_Feedbacks { get; set; }
        //public DbSet<General_Feedback> general_Feedbacks { get; set; }
        public DbSet<Student_Task_Submissions> Student_Task_Submissions { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<StudentConsultations> StudentConsultations { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<InstructorSkills> InstructorSkills { get; set;}
        
    }
}
