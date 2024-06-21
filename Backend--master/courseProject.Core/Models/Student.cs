﻿using courseProject.core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Student
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        // [ForeignKey("User")]
        // public int Id { get; set; }

        [Key]       
        [ForeignKey("User")]
        public Guid StudentId { get; set; }

       

        public User user { get; set; }

        public List<Request> requests { get; set; }

        public List<StudentCourse> studentCourses { get; set; }

       // public List<Consultation> Consultations { get; set; }

       // public List<Instructor_Feedback> instructor_Feedbacks { get; set; }

      //  public List<Course_Feedback> course_Feedbacks { get; set;}
        public List<Student_Task_Submissions> Student_Task_Submissions { get; set; }
        public List<StudentConsultations> studentConsultations { get; set; }
    }
}
