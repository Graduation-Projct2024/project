﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class BookALectureDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int InstructorId { get; set; }
        public string Duration { get; set; }
    }
}
