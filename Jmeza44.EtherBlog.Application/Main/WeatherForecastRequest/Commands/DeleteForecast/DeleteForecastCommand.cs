using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.DeleteForecast
{
    public class DeleteForecastCommand : CacheInvalidatorCommand, IRequest<bool>
    {
        public int Id { get; set; }

        private static readonly Type[] entitiesThatCommandInvalidates = new Type[] { typeof(WeatherForecast) };
        public override void SetEntitiesThatCommandInvalidates()
        {
            InvalidatesQueriesThatDependOn = entitiesThatCommandInvalidates;
        }
    }

    public class DeleteForecastCommandHandler : IRequestHandler<DeleteForecastCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteForecastCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteForecastCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _dbContext.WeatherForecast.Where(w => request.Id == w.Id).ExecuteDeleteAsync(cancellationToken);
            return deleted > 0;
        }
    }
}
