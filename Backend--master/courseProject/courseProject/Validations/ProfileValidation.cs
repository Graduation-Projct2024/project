using courseProject.Core.Models.DTO.UsersDTO;
using FluentValidation;

namespace courseProject.Validations
{
    public class ProfileValidation : AbstractValidator<ProfileDTO>
    {
        public ProfileValidation()
        {

            RuleFor(x => x.image)
                .Must(HaveValidImageExtension)
            .WithMessage("Image must have a .jpg , .png or .jpeg extension.");
        }



        private  bool HaveValidImageExtension(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return false;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = System.IO.Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }

    }
    }

