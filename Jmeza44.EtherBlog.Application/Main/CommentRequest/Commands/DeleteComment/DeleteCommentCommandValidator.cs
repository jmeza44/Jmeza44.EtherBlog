using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
