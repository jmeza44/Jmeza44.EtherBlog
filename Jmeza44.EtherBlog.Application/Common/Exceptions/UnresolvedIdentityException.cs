namespace Jmeza44.EtherBlog.Application.Common.Exceptions
{
    public class UnresolvedIdentityException : Exception
    {
        public UnresolvedIdentityException(): base("User Identity couldn't be resolved.") { }
    }
}
