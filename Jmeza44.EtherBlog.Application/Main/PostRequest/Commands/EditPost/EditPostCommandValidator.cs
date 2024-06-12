using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.EditPost
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(command => command.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content cannot be longer than 1000 characters.");
        }
    }
}
