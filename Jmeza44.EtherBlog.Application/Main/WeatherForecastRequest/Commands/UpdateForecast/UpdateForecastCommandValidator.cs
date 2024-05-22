using FluentValidation;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.UpdateForecast
{
    public class UpdateForecastCommandValidator : AbstractValidator<UpdateForecastCommand>
    {
        public UpdateForecastCommandValidator()
        {
            RuleFor(command => command.WeatherForecast.Id).NotNull().NotEmpty().WithMessage("The Id cannot be null or empty");
            RuleFor(command => command.WeatherForecast.City).NotNull().NotEmpty().WithMessage("The City cannot be null or empty");
            RuleFor(command => command.WeatherForecast.Summary).NotNull().NotEmpty().WithMessage("The Summary cannot be null or empty");
        }
    }
}
