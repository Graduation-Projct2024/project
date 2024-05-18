﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseProject.Core.Models.DTO
{
    public class CourseInformationDto
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

        public string? Deadline { get; set; }
        public int? limitNumberOfStudnet { get; set; }
        public int? totalHours { get; set; }

    }
}
