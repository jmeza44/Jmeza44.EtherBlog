namespace Jmeza44.EtherBlog.Application.Common.AppSettingsMapping
{
    public class AppSettings
    {
        public required AppOptions App { get; set; }
        public required ConnectionStrings ConnectionStrings { get; set; }
        public required LoggingOptions Logging { get; set; }
        public required string AllowedHosts { get; set; }
    }
}
