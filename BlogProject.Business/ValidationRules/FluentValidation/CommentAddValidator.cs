using BlogProject.DTO.DTOs.Comment;
using FluentValidation;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CommentAddValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddValidator()
        {
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("This field cant be empty");
            RuleFor(x => x.AuthorEmail).NotEmpty().WithMessage("This field cant be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("This field cant be empty");
        }
    }
}
