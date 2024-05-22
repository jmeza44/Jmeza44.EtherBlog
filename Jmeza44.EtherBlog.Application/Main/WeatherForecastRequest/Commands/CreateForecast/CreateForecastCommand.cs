using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.CreateForecast
{
    public class CreateForecastCommand : CacheInvalidatorCommand, IRequest<bool>
    {
        public List<WeatherForecast> WeatherForecasts { get; set; }

        private static readonly Type[] entitiesThatCommandInvalidates = new Type[] { typeof(WeatherForecast) };
        public override void SetEntitiesThatCommandInvalidates()
        {
            InvalidatesQueriesThatDependOn = entitiesThatCommandInvalidates;
        }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateForecastCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTodoListCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(CreateForecastCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.WeatherForecast.AddRangeAsync(request.WeatherForecasts, cancellationToken);
            var created = await _dbContext.SaveChangesAsync(cancellationToken);
            return created > 0;
        }
    }
}
