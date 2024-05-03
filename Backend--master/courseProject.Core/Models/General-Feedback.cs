using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class General_Feedback
    {
        public int Id {  get; set; }

        public string content {  get; set; }

        public DateTime dateOfAdded { get; set; }=DateTime.Now;
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


         

    }
}
