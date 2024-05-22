using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jmeza44.EtherBlog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"), b =>
                    {
                        b.EnableRetryOnFailure();
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
