using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Course_Feedback
    {
        public int Id { get; set; }

        public string content { get; set; }

        public DateTime dateOfAdded { get; set; }

        [ForeignKey("Student")]
        public int studentId { get; set; }

        [ForeignKey("Course")]
        public int courseId { get; set; }

        public Student Student { get; set; }
        public Course course { get; set; }

    }
}
