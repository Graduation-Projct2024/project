using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }

        public string status { get; set; } = "off";
        public string category { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime? dateOfAdded { get; set; } = DateTime.Now;

        public DateTime? startDate { get; set;} 
        public DateTime? endDate { get; set;}

        [ForeignKey("Instructor")]
        public int instructorId { get; set; } 

        [ForeignKey("SubAdmin")]
        public int subAdminId { get; set; } 
        [ForeignKey("Request")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requestId { get; set; } 

        public Instructor Instructor { get; set; }

        public SubAdmin SubAdmin { get; set; }

        public List<CourseMaterial> Materials { get; set; }

        public List<StudentCourse> studentCourses { get; set; }

        public List<Course_Feedback> course_Feedbacks { get; set; }
        public Request Request { get; set; }

    }
}
