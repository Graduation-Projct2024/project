using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace courseProject.Core.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string type { get; set; }

        public string status { get; set; } = "off";

        public DateTime date { get; set; } 
        public TimeSpan startTime { get; set; }

        public TimeSpan endTime { get; set; }
         public TimeSpan Duration { get; set; }
        public DateTime dateOfAdded { get; set; } = DateTime.Now;
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        public Student student { get; set; }
         public Instructor instructor { get; set; }
        public List<StudentConsultations> studentConsultations { get; set; }
    }
}
