namespace courseProject.Core.Models.DTO
{
    public class CourseInformationDto
    {

        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public double price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public int SubAdminId { get; set; }
        public string SubAdminName { get; set; }


    }
}
