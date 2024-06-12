using AutoMapper;
using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Queries.GetForecast
{
    public class GetForecastQuery : CacheableQuery, IRequest<List<WeatherForecastDto>>
    {
        public int? Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public override void SetEntitiesThatCacheDependsOn()
        {
            CacheDependsOn = new Type[] { typeof(WeatherForecast) };
        }
        public override void SetQueryCacheOptions()
        {
            QueryCacheOptions = new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(2) };
        }
    }

    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, List<WeatherForecastDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetForecastQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<WeatherForecastDto>> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            var result = new List<WeatherForecast>();

            if (request.Id is not null)
            {
                result = await _dbContext.WeatherForecast
                                .Where(w => request.Id == w.Id)
                                .ToListAsync(cancellationToken);
            }
            else
            {
                if (request.Take == 0)
                {
                    result = await _dbContext.WeatherForecast
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    result = await _dbContext.WeatherForecast
                                .Skip(request.Skip)
                                .Take(request.Take)
                                .ToListAsync(cancellationToken);
                }
            }

            return result is null ? throw new Exception() : _mapper.Map<List<WeatherForecastDto>>(result);
        }
    }
}
