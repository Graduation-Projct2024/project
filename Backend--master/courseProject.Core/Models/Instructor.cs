using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Instructor
    {

        [Key]
        [ForeignKey("User")]
        public Guid InstructorId { get; set; }
        public string? LName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        public static string type { get; set; } = "Instructor";

        public string? gender { get; set; }

        public string? address { get; set; }
        public string? ImageUrl { get; set; }

        public string? skillDescription { get; set; }
        public User user { get; set; }

        public List<Instructor_Working_Hours> Instructor_Working_Hours { get; set; }

        public List<CourseMaterial> Materials { get; set;}
        public List<Consultation> Consultations { get; set;}

       // public List<Instructor_Feedback> instructor_Feedbacks { get; set; }
        public List<InstructorSkills> instructorSkills { get; set; }

    }
}
