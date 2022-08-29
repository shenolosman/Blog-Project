using BlogProject.DTO.DTOs.AppUser;
using FluentValidation;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Can not be empty!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Can not be empty!");
        }
    }
}
