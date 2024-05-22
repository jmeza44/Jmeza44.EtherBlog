using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Jmeza44.EtherBlog.Application.Common.InMemoryCache
{
    public class MemoryCacheLogger
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheLogger(ILogger<MemoryCacheLogger> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void LogCacheAdded(string cacheKey)
        {
            _logger.LogInformation("Added to Cache -> {cacheKey}", cacheKey);
            LogCacheStatistics();
        }

        public void LogCacheRemoved(string cacheKey)
        {
            _logger.LogInformation("Removed from Cache -> {cacheKey}", cacheKey);
            LogCacheStatistics();
        }

        public void LogAccessedFromCache(string cacheKey)
        {
            _logger.LogInformation("Accessed from Cache -> {cacheKey}", cacheKey);
            LogCacheStatistics();
        }

        public void LogCacheStatistics()
        {
            var statistics = _memoryCache.GetCurrentStatistics();
            if (statistics != null)
            {
                _logger.LogInformation("Entries in cache: {CurrentEntryCount}, Memory used: {CurrentEstimatedSize}",
                                       statistics.CurrentEntryCount,
                                       statistics.CurrentEstimatedSize);
            }
        }
    }
}
