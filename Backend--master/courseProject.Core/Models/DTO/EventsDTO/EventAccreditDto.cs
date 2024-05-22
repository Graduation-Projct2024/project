namespace courseProject.Core.Models.DTO.EventsDTO
{
    public class EventAccreditDto
    {
        public int Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }
        public string status { get; set; }
        public DateTime dateOfEvent { get; set; }
        public string category { get; set; }

        public int SubAdminId { get; set; }

        public string subAdminFName { get; set; }
        public string subAdminLName { get; set; }

    }
}
