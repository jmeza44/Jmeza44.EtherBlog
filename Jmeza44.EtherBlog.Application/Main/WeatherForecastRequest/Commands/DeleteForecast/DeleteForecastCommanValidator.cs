using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.DeleteForecast
{
    public class DeleteForecastCommanValidator : AbstractValidator<DeleteForecastCommand>
    {
        public DeleteForecastCommanValidator()
        {

            RuleFor(command => command.Id)
                .NotNull().NotEmpty().WithMessage("Missing Weather Forecast ID");
        }
    }
}
