﻿using courseProject.core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class CourseMaterial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }
        public string type { get; set; }
      [NotMapped] public IFormFile? pdf {  get; set; }
       public string? pdfUrl { get; set; }
        public  DateTime dateOfAdded { get; set; }= DateTime.Now;
        public  DateTime? DeadLine { get; set; } 

        //  public DateTime? date { get; set; } = DateOnly.MinValue;
        public string? linkUrl { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId {  get; set; }

        [ForeignKey("Course")]
        public int? courseId {  get; set; }

        [ForeignKey("Consultation")]
        public int? consultationId { get; set; }
        public Course Course { get; set; }
        public Consultation consultation { get; set; }
        public Instructor Instructor { get; set; }
        public List<Student_Task_Submissions> Student_Task_Submissions { get; set; }

    }
}
