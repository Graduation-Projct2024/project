using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class CourseForEditDTO
    {
        public string? name { get; set; }
        public double? price { get; set; }
        public string? category { get; set; }
        public string? description { get; set; }
        [NotMapped] public IFormFile? image { get; set; }
        public int? InstructorId { get; set; }


    }
}
