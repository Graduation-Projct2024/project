﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class LinkDTO
    {
        public string name { get; set; }
        public string linkUrl { get; set; }
        public int courseId { get; set; }
        public int InstructorId { get; set; }
    }
}
