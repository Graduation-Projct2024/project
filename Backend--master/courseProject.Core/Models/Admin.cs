using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace courseProject.Core.Models
{
    public class Admin 
    {
        [Key]
        [ForeignKey("User")]       
        public Guid AdminId { get; set; }
        public string? LName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [AllowNull]
        public string? phoneNumber { get; set; }

        public string? gender { get; set; }

        public string? address { get; set; }
        public string? ImageUrl { get; set; }

        public User user { get; set; }
       // public List<Request> requests { get; set; }

    }
}
