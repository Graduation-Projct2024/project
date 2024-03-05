using courseProject.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.Models;
using System.Diagnostics.CodeAnalysis;
namespace courseProject.Core.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; } 

        public string satus { get; set; } = "off";

       // public string type { get; set; }

        public DateTime date { get; set; } = DateTime.Now;

        [ForeignKey("Admin")]
        public int adminId { get; set; } = 1;

        [ForeignKey("Student")]
        [AllowNull]
        public int? studentId { get; set; } = 3;
        [ForeignKey("SubAdmin")]
        public int subAdminId { get; set; } 

        public Admin Admin { get; set; }
        public SubAdmin SubAdmin { get; set; }  
        public Student Student { get; set; }

        public Event Event { get; set; }
        public Course Course { get; set; }
    }
}
