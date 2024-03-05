using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseProject.Core.Models.DTO
{
    public class EventDto
    {

        public int Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }

        public DateTime dateOfEvent { get; set; }
            

        public int subAdminId { get; set; }

        public string subAdminName { get; set;}
    }
}
