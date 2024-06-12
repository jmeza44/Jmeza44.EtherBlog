using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Queries.GetAllPost
{
    public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
    {
        public GetAllPostsQueryValidator()
        {
            RuleFor(query => query.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than zero.");

            RuleFor(query => query.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than zero.")
                .LessThanOrEqualTo(100).WithMessage("PageSize cannot be more than 100.");
        }
    }
}
