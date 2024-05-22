namespace Jmeza44.EtherBlog.Application.Common.InMemoryCache
{
    public class InMemoryCacheGlobalOptions
    {
        public const string AccessKey = "App:InMemoryCacheConfiguration";

        public double? AbsoluteExpirationRelativeToNowMinutes { get; set; } = null;
        public double? SlidingExpirationMinutes { get; set; } = null;
        public long? SizeLimitUnits { get; set; } = null;
        public bool? TrackStatistics { get; set; } = null;
    }
}
