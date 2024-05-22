namespace Jmeza44.EtherBlog.Application.Common.Exceptions
{
    public class AppSettingMissingException : Exception
    {
        public string MissingKey { get; }

        public AppSettingMissingException(string missingKey)
            : base($"Missing value in appsettings.json for key: {missingKey}")
        {
            MissingKey = missingKey;
        }
    }
}
