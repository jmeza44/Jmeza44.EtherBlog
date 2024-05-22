namespace Jmeza44.EtherBlog.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Task<string?> GetCurrentUserEmailAsync();
        bool IsInRole(string roleName);
    }
}
