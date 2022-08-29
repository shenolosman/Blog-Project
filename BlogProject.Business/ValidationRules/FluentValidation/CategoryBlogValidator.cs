using BlogProject.DTO.DTOs.CategoryBlog;
using FluentValidation;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryBlogValidator : AbstractValidator<CategoryBlogDto>
    {
        public CategoryBlogValidator()
        {
            RuleFor(x => x.CategoryId).InclusiveBetween(0, int.MaxValue)
                .WithMessage("Category id cannot be empty or lower than zero");
            RuleFor(x => x.BlogId).InclusiveBetween(0, int.MaxValue)
                .WithMessage("Blog id cannot be empty or lower than zero");
        }
    }
}
