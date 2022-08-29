using BlogProject.DTO.DTOs.Category;
using FluentValidation;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryAddValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field can't be empty");
        }
    }
}
