using Jmeza44.EtherBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WeatherForecast> WeatherForecast { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
