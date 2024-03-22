using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class StudentCourse
    {
        [Key]
        [ForeignKey("Student")]
        public int StudentId {  get; set; }
        [Key]
        [ForeignKey("Course")]
        public int courseId { get; set; }

        public DateTime EnrollDate { get; set; }= DateTime.Now;

        public Student Student { get; set; }

        public Course Course { get; set; }

    }
}
