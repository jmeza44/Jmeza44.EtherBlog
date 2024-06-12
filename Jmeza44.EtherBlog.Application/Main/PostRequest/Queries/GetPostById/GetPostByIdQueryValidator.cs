using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.PostRequest.Queries.GetPostById
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(query => query.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
