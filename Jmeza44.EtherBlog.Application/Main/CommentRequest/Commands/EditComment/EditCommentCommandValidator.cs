using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.EditComment
{
    public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(command => command.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content cannot be longer than 1000 characters.");

            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
