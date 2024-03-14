﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO
{
    public class EventForCreateDTO
    {

      //  public int Id { get; set; }
        public string name { get; set; }

        public string content { get; set; }
        [NotMapped] public IFormFile? image { get; set; }
        public string category { get; set; }
       
        public int SubAdminId { get; set; }

    }
}
