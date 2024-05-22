namespace Jmeza44.EtherBlog.Application.Common.InMemoryCache
{
    public abstract class CacheInvalidatorCommand
    {
        /// <summary>
        /// List of the entities types modified by the command.
        /// If the command is related with class A, any cached query that relies on A will be invalidated.
        /// </summary>
        protected Type[] InvalidatesQueriesThatDependOn = Array.Empty<Type>();

        public abstract void SetEntitiesThatCommandInvalidates();
        public Type[] GetEntitiesThatCommandInvalidates() => InvalidatesQueriesThatDependOn;
    }
}
