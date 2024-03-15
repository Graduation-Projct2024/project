using Microsoft.EntityFrameworkCore;
using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            modelBuilder.Entity<Instructor_Working_Hours>(
                e =>
                {
                    e.HasKey(x => new {x.InstructorId , x.day , x.startTime , x.endTime});
                   
                }
                );
            
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
            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.StudentId)
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

        }
}
