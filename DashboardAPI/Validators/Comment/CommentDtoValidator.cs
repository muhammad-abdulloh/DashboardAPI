using DashboardAPI.Models.Constants;
using DashboardAPI.Models.DTOs.Comment;
using FluentValidation;

namespace DashboardAPI.Validators.Comment
{
    public class CommentDtoValidator : AbstractValidator<ICommentDto>
    {
        public CommentDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(p => UserMessage.CannotBeNull(nameof(p)));
            RuleFor(p => p.Content).NotNull().NotEmpty().WithMessage(p => UserMessage.CannotBeNullOrEmpty(p.Content));
        }
    }
}
