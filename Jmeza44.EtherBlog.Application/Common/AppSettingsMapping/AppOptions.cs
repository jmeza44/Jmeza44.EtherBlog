using Jmeza44.EtherBlog.Application.Common.InMemoryCache;

namespace Jmeza44.EtherBlog.Application.Common.AppSettingsMapping
{
    public class AppOptions
    {
        public required InMemoryCacheGlobalOptions InMemoryCacheConfiguration { get; set; }
        public required CorsPolicyOptions CorsPolicy { get; set; }
        public required AuthenticationOptions AuthenticationOptions { get; set; }
    }
}
