using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Jmeza44.EtherBlog.Application.Common.Behaviours
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : CacheInvalidatorCommand
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheLogger _memoryCacheLogger;

        public CacheInvalidationBehavior(IMemoryCache memoryCache, MemoryCacheLogger memoryCacheLogger)
        {
            _memoryCache = memoryCache;
            _memoryCacheLogger = memoryCacheLogger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request.SetEntitiesThatCommandInvalidates();

            TResponse response = await next(); // Send the request

            // Begin: Cache invalidation

            var cacheKeysToInvalidate = CacheKeys.GetCacheKeysThatDependOn(request.GetEntitiesThatCommandInvalidates());

            foreach (var cacheKey in cacheKeysToInvalidate)
            {
                _memoryCache.Remove(cacheKey);
                CacheKeys.RemoveCacheKey(cacheKey);
                _memoryCacheLogger.LogCacheRemoved(cacheKey);
            }
            // End: Cache invalidation

            return response;
        }
    }
}
