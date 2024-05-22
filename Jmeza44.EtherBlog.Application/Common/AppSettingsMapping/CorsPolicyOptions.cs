namespace Jmeza44.EtherBlog.Application.Common.AppSettingsMapping
{
    public class CorsPolicyOptions
    {
        public required string Name { get; set; }
        public required string[] AllowedOrigins { get; set; }
    }
}
