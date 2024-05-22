using Microsoft.Extensions.Caching.Memory;

namespace Jmeza44.EtherBlog.Application.Common.InMemoryCache
{
    public abstract class CacheableQuery
    {
        /// <summary>
        /// List of the Entities types queried in the request.
        /// If the query depends on A class, the cache is invalidated when a command related with the same class is executed.
        /// </summary>
        protected Type[] CacheDependsOn = Array.Empty<Type>();
        /// <summary>
        /// Cache options configured for the Query.
        /// If is not configured global options are used.
        /// </summary>
        protected MemoryCacheEntryOptions QueryCacheOptions;

        public abstract void SetEntitiesThatCacheDependsOn();
        public virtual void SetQueryCacheOptions() => QueryCacheOptions = null;
        public Type[] GetEntitiesThatCacheDependsOn() => CacheDependsOn;
        public MemoryCacheEntryOptions GetQueryCacheOptions() => QueryCacheOptions;
    }
}
