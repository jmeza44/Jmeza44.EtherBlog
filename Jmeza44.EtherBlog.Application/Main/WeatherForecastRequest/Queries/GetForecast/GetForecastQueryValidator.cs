using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Queries.GetForecast
{
    public class GetForecastQueryValidator : AbstractValidator<GetForecastQuery>
    {
        public GetForecastQueryValidator()
        {
            RuleFor(query => query.Skip).GreaterThan(-1).WithMessage("Negative values not allowed");
            RuleFor(query => query.Take).GreaterThan(-1).WithMessage("Negative values not allowed");
        }
    }
}
