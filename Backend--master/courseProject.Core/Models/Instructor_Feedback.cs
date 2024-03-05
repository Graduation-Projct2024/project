using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Instructor_Feedback
    {

        public int Id { get; set; }
        
        public string content { get; set; }
        public DateTime dateOfAdded { get; set; }

        [ForeignKey("Student")]
        public int studentId { get; set; }

        [ForeignKey("Instructor")]
        public int instructorId { get; set; }

        public Student Student { get; set; }
        public Instructor Instructor { get; set; }


    }
}
