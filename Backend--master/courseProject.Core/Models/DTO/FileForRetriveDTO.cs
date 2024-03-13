using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class FileForRetriveDTO
    {

        public string name { get; set; }

        public string? description { get; set; }

        public String? pdfUrl { get; set; }
    }
}
