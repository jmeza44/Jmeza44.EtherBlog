using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(command => command.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
