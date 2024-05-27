using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class InstructorSkills
    {
        [Key]
        [ForeignKey("Skills")]
        public Guid skillId {  get; set; }
        [Key]
        [ForeignKey("Instructor")]
        public Guid InstructorId { get; set; }

        public Instructor Instructor { get; set; }
        public Skills Skills { get; set; }
    }
}
