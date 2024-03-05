namespace courseProject.Core.Models.DTO
{
    public class EventAccreditDto
    {
        public int Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }

        public DateTime dateOfEvent { get; set; }
        public string EventCategory { get; set; }

        public int subAdminId { get; set; }

        public string subAdminFName { get; set; }
        public string subAdminLName { get; set; }

    }
}
