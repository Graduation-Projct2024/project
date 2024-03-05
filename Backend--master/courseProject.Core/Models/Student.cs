using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? LName { get; set; }

        [ForeignKey("User")]
        public string email {  get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? phoneNumber {  get; set; }

        public string? gender {  get; set; }

        public string? address { get; set; }
        public string? ImageUrl { get; set; }

        public User user { get; set; }

        public List<Request> requests { get; set; }

        public List<StudentCourse> studentCourses { get; set; }

        public List<Consultation> Consultations { get; set; }

        public List<Instructor_Feedback> instructor_Feedbacks { get; set; }

        public List<Course_Feedback> course_Feedbacks { get; set;}

    }
}
