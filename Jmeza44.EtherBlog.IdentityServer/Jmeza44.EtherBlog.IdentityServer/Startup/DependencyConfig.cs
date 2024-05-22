using IdentityServer.Data.Persistence;
using IdentityServer.Models;
using IdentityServer.Startup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer.Startup
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddAspNetServices(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDistributedMemoryCache();

            return services;
        }

        public static IServiceCollection AddDbcontexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase")));

            return services;
        }

        public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                builder.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"),
                        sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                builder.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"),
                        sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddAspNetIdentity<ApplicationUser>();

            return services;
        }
    }
}
