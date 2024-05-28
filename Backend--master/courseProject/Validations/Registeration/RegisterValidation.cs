using courseProject.Core.Models.DTO.RegisterDTO;
using FluentValidation;
using System.Data;

namespace courseProject.Validations.Registeration
{
    public class RegisterValidation : AbstractValidator<RegistrationRequestDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("The password minimum length is 8 charecter")
                .MaximumLength(20).WithMessage("The password is too long");
        }



    }
}
