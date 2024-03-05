using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class CourseForCreateDTO
    {
        public int Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }
        public string category { get; set; }
        public string? ImageUrl { get; set; }
        public string SubAdminId { get; set; }
        public string InstructorId { get; set; }
       // public int? requestId { get; set; }
    }
}
