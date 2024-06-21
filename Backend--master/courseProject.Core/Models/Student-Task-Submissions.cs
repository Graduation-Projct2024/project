using courseProject.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.core.Models
{
    public class Student_Task_Submissions
    {
        [Key]
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        [Key]
        [ForeignKey("CourseMaterial")]
        public Guid TaskId { get; set; }
        public string? description { get; set; }
        [NotMapped] public IFormFile? pdf { get; set; }
        public string? pdfUrl { get; set; }
        public DateTime dateOfAdded { get; set; }=DateTime.Now;
        public Student Student { get; set; }
        public CourseMaterial CourseMaterial { get; set; }
    }
}
