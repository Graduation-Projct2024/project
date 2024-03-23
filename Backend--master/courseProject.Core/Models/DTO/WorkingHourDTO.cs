using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class WorkingHourDTO
    {
        public DayOfWeek day { get; set; }
        public string startTime { get; set; } 
        public string endTime { get; set; }
    }
}
