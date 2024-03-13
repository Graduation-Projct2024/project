using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string userName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [Key]
        public string email {  get; set; }
        public string password { get; set; }
        public string role { get; set; }

        public Student student { get; set; }

        public Instructor instructor { get; set; }

        public Admin admin { get; set; }
        public SubAdmin subadmin { get; set; }

        public List<General_Feedback> general_feedback { get; set; }


    }
}
