using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.UpdateForecast
{
    public class UpdateForecastCommand : CacheInvalidatorCommand, IRequest<bool>
    {
        public WeatherForecastDto WeatherForecast { get; set; }

        private static readonly Type[] entitiesThatCommandInvalidates = new Type[] { typeof(WeatherForecast) };
        public override void SetEntitiesThatCommandInvalidates()
        {
            InvalidatesQueriesThatDependOn = entitiesThatCommandInvalidates;
        }
    }

    public class UpdateForecastCommandHandler : IRequestHandler<UpdateForecastCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateForecastCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateForecastCommand request, CancellationToken cancellationToken)
        {
            var previous = await _dbContext.WeatherForecast
                                    .FirstOrDefaultAsync(w => w.Id == request.WeatherForecast.Id, cancellationToken);

            if (previous != null)
            {
                previous.Date = DateTime.UtcNow;
                previous.TemperatureC = request.WeatherForecast.TemperatureC;
                previous.City = request.WeatherForecast.City ?? previous.City;
                previous.Summary = request.WeatherForecast.Summary ?? previous.Summary;
            }

            var updated = await _dbContext.SaveChangesAsync(cancellationToken);
            return updated > 0;
        }
    }
}
