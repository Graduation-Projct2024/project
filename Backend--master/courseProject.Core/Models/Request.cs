using courseProject.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;
namespace courseProject.Core.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string name { get; set; } 

        public string satus { get; set; } = "off";

        public string? description { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

       // [ForeignKey("Admin")]
       // public Guid AdminId { get; set; } 

        [ForeignKey("Student")]
        [AllowNull]
        public Guid? StudentId { get; set; }
        [ForeignKey("SubAdmin")]
        [AllowNull]
        public Guid? SubAdminId { get; set; } 

      //  public Admin Admin { get; set; }
        public SubAdmin SubAdmin { get; set; }  
        public Student Student { get; set; }

        public Event Event { get; set; }
        public Course Course { get; set; }
    }
}
