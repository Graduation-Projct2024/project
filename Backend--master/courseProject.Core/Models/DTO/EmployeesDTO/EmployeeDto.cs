﻿namespace courseProject.Core.Models.DTO.EmployeesDTO
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FName { get; set; }

        public string? LName { get; set; }

        public string email { get; set; }
        public string type { get; set; }

        public string phoneNumber { get; set; }

        public string? gender { get; set; }

        public string? address { get; set; }

    }
}
