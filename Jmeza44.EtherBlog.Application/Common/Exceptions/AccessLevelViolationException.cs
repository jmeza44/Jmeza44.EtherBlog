namespace Jmeza44.EtherBlog.Application.Common.Exceptions
{
    public class AccessLevelViolationException : Exception
    {
        public AccessLevelViolationException() : base("User has not permission to perform this action.") { }
    }
}
