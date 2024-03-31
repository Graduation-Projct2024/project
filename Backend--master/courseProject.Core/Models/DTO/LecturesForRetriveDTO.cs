using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class LecturesForRetriveDTO
    {

        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int InstructorId { get; set; }
        public string Duration { get; set; }
        public int StudentId { get; set; }
        public string date { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string InstructoruserName { get; set; }
        public string? InstructorLName { get; set; }
        public string StudentuserName { get; set; }
        public string? StudentLName { get; set; }
    }
}
