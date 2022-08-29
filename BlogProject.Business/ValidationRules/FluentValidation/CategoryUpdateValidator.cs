using BlogProject.DTO.DTOs.Category;
using FluentValidation;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(0, int.MaxValue).WithMessage("Cant be negative of empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("This field must be filled!");
        }
    }
}
