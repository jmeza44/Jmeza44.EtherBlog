using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Queries.GetCountForecasts
{
    public class GetCountForecastsQuery : CacheableQuery, IRequest<int>
    {
        public override void SetEntitiesThatCacheDependsOn()
        {
            CacheDependsOn = new Type[] { typeof(WeatherForecast) };
        }
    }

    public class GetCountForecastsQueryHandler : IRequestHandler<GetCountForecastsQuery, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCountForecastsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(GetCountForecastsQuery request,
                CancellationToken cancellationToken)
        {
            var result = await _dbContext.WeatherForecast.CountAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
