using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.AddComment
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(command => command.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content cannot be longer than 1000 characters.");

            RuleFor(command => command.PostId)
                .GreaterThan(0).WithMessage("PostId must be greater than zero.");
        }
    }
}
