using System.ComponentModel.DataAnnotations;

namespace courseProject.Core.Models.DTO
{
    public class RegistrationRequestDTO
    {

        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string ConfirmPassword { get; set; }
        public string role { get; set; }
    }
}
