using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Instructor_Working_Hours
    {
        [Key]
        [ForeignKey("Instructor")]
        public int instructorId {  get; set; }
        [Key]
        public DayOfWeek day {  get; set; }
        [Key]
        public DateTime startTime { get; set; } = new DateTime().ToLocalTime(); 
        [Key]
        public DateTime endTime { get; set; }


        public Instructor instructor { get; set; }
    }
}
