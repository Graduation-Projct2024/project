﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class Instructor_OfficeHoursDTO
    {
        public int InstructorId { get; set; }
        public string userName { get; set; }
        public string? LName { get; set; }
        public DayOfWeek day { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
