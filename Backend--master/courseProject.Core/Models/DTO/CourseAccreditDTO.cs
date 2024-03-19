namespace courseProject.Core.Models.DTO
{
    public class CourseAccreditDTO
    {

        public int Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; } 
        public DateTime endDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? SubAdminFName { get; set; }
        public string? SubAdminLName { get; set; }
        public string? InstructorFName { get; set; }
        public string? InstructorLName { get; set; }




    }
}
