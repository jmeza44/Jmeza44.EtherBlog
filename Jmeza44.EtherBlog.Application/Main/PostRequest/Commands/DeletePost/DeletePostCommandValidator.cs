using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Commands.DeletePost
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
