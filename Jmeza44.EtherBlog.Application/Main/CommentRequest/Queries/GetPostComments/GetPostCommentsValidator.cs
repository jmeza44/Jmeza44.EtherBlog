using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.CommentRequest.Queries.GetPostComments
{
    public class GetPostCommentsQueryValidator : AbstractValidator<GetPostCommentsQuery>
    {
        public GetPostCommentsQueryValidator()
        {
            RuleFor(query => query.PostId)
                .GreaterThan(0).WithMessage("PostId must be greater than zero.");
        }
    }
}
