using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string type { get; set; }

        public string status { get; set; }

        public DateTime date { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public DateTime dateOfAdded { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        public Student student { get; set; }
         public Instructor instructor { get; set; }

    }
}
