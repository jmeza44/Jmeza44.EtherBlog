using Jmeza44.EtherBlog.Application.Common.InMemoryCache;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace Jmeza44.EtherBlog.Application.Common.Behaviours
{
    public class QueryCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : CacheableQuery
    {
        private readonly IMemoryCache _memoryCache;
        private readonly InMemoryCacheGlobalOptions _cacheOptions;
        private readonly MemoryCacheLogger _memoryCacheLogger;

        public QueryCacheBehaviour(IMemoryCache memoryCache, IOptions<InMemoryCacheGlobalOptions> cacheOptions, MemoryCacheLogger memoryCacheLogger)
        {
            _memoryCache = memoryCache;
            _cacheOptions = cacheOptions.Value;
            _memoryCacheLogger = memoryCacheLogger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request.SetEntitiesThatCacheDependsOn();
            request.SetQueryCacheOptions();

            TResponse response;
            string cacheKey = GenerateCacheKey(request);

            var cachedResponse = _memoryCache.Get<byte[]>(cacheKey);

            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
                _memoryCacheLogger.LogAccessedFromCache(cacheKey);
            }
            else
            {
                response = await RequestTheDatabaseAndStoreInCache(next, cacheKey, request);
            }

            return response;
        }

        private async Task<TResponse> RequestTheDatabaseAndStoreInCache(RequestHandlerDelegate<TResponse> next, string cacheKey, TRequest request)
        {
            TResponse response = await next(); // Send the request

            var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response)); // Serialize the response

            _memoryCache.Set(cacheKey, serializedData, GetMemoryCacheEntryOptions(request)); // Add the serialized response to cache
            CacheKeys.AddCacheKey(cacheKey); // Store the key for the cached data
            _memoryCacheLogger.LogCacheAdded(cacheKey);

            return response;
        }

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TRequest request)
        {
            var customAbsExpConfigured = request.GetQueryCacheOptions()?.AbsoluteExpirationRelativeToNow != null;
            var customSlidExpConfigured = request.GetQueryCacheOptions()?.SlidingExpiration != null;
            var globalAbsExpConfigured = _cacheOptions.AbsoluteExpirationRelativeToNowMinutes != null;
            var globalSlidExpConfigured = _cacheOptions.SlidingExpirationMinutes != null;

            TimeSpan? absoluteExpirationRelativeToNow = null;
            TimeSpan? slidingExpiration = null;

            if (customAbsExpConfigured) absoluteExpirationRelativeToNow = request.GetQueryCacheOptions().AbsoluteExpirationRelativeToNow;
            else if (globalAbsExpConfigured) absoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheOptions.AbsoluteExpirationRelativeToNowMinutes.Value);

            if (customSlidExpConfigured) slidingExpiration = request.GetQueryCacheOptions().SlidingExpiration;
            else if (globalSlidExpConfigured) slidingExpiration = TimeSpan.FromMinutes(_cacheOptions.SlidingExpirationMinutes.Value);

            const long UNITARY_SIZE = 1;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow,
                SlidingExpiration = slidingExpiration,
                Size = UNITARY_SIZE
            };

            return cacheEntryOptions;
        }

        private static string GenerateCacheKey(TRequest request)
        {
            // Generate a unique cache key based on the entities that the query relais on, the tenant name, the request name, and the request parameters
            string[] namesOfTheEntitiesThatCacheRelaisOn = request.GetEntitiesThatCacheDependsOn().Select(t => t.Name).ToArray();
            string requestName = request.GetType().Name;
            string requestParams = GetRequestParamsAsString(request);

            return CacheKeys.BuildCacheKey(namesOfTheEntitiesThatCacheRelaisOn, requestName, requestParams);
        }

        private static string GetRequestParamsAsString(TRequest request)
        {
            var sb = new StringBuilder();

            PropertyInfo[] properties = request.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(request);
                string name = property.Name;

                //if (name == nameof(request.CacheRelaisOn)) continue; // Ignores the property

                sb.Append($"{name}:{value ?? "null"};");
            }

            return sb.ToString().TrimEnd(';');
        }
    }
}
