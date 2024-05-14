using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    //<summery>
    //
    //this DTO is for used to view all courses to students 
    //
    //</Summery>
    public class CourseInfoForStudentsDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string? ImageUrl { get; set; }
        public double price { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public int SubAdminId { get; set; }
        public string SubAdminName { get; set; }
        public bool isEnrolled { get; set; }
    }
}
