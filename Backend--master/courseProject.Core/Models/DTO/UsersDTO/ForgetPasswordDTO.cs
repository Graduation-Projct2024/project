﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.UsersDTO
{
    public class ForgetPasswordDTO
    {
        public string password { get; set; }
        public string ConfirmPassword {  get; set; }
    }
}
