namespace Jmeza44.EtherBlog.Application.Common.AppSettingsMapping
{
    public class AuthenticationOptions
    {
        public required string Authority { get; set; }
        public required string Audience { get; set; }
        public required TokenValidationParameters TokenValidationParameters { get; set; }
    }
}
