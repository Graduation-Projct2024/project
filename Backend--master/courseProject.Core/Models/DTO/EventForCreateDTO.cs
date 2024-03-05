using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class EventForCreateDTO
    {

        public int Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }
        public string? ImageUrl { get; set; }
        public string category { get; set; }
       
        public int subAdminId { get; set; }

    }
}
