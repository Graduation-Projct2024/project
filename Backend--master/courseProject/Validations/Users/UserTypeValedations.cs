using courseProject.Core.Models.DTO.MaterialsDTO;
using FluentValidation;

namespace courseProject.Validations.Users
{
    public class UserTypeValedations : AbstractValidator<UserTypeDTO>
    {
        public UserTypeValedations()
        {
            RuleFor(x => x.userType)
                .NotEmpty().WithMessage("The user type is required.")
                .Must(type => new[] { "student", "instructor"}.Contains(type)).WithMessage("The possible type is student or instructor ");
        }
    }
}
