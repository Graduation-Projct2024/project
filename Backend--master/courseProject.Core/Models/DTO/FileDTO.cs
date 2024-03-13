using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class FileDTO
    {

        public string name { get; set; }

        public string? description { get; set; }

        [NotMapped] public IFormFile pdf { get; set; }
    
        public int courseId { get; set; }
        public int instructorId { get; set; }
    }
}
