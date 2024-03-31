using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class StudentSubmissionDTO
    {
        public int StudentId { get; set; }
        public string userName { get; set; }
        public string? LName { get; set; }
        public string email { get; set; }
        public int TaskId { get; set; }
        public string? description { get; set; }
        public string? pdfUrl { get; set; }
    }
}
