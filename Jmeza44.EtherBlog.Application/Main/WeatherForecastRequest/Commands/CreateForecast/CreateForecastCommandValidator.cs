using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.CreateForecast
{
    public class CreateForecastCommandValidator : AbstractValidator<CreateForecastCommand>
    {
        public CreateForecastCommandValidator()
        {

            RuleForEach(command => command.WeatherForecasts)
                .NotNull()
                .NotEmpty()
                .ChildRules(list =>
                {
                    list.RuleFor(wf => wf.City).NotNull().NotEmpty().WithMessage("The City cannot be null or empty");
                    list.RuleFor(wf => wf.Summary).NotNull().NotEmpty().WithMessage("The Summary cannot be null or empty");
                });
        }
    }
}
