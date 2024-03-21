using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace courseProject.Core.Models
{
    public class Admin
    {
        [Key]
        [ForeignKey("User")]
        public int AdminId { get; set; }
        public string? LName { get; set; }

        [ForeignKey("User")]
        public string email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [AllowNull]
        public string? phoneNumber { get; set; }

        public string? gender { get; set; }

        public string? address { get; set; }
        public string? ImageUrl { get; set; }

        public User user { get; set; }
        public List<Request> requests { get; set; }

    }
}
