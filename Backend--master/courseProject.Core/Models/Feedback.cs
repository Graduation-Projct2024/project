using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class Feedback
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string content { get; set; }
            public DateTime dateOfAdded { get; set; } = DateTime.Now;
            public string type { get; set; }
            [ForeignKey("Course")]
            [AllowNull]
            public int? CourseId { get; set; }

            // public Instructor Instructor { get; set; }
            [ForeignKey("User , User")]
            public int StudentId { get; set; }

            public int? InstructorId { get; set; }

            public User User { get; set; }
            public Course course { get; set; }
        }
    }

