using Duende.IdentityServer.EntityFramework.DbContexts;
using IdentityServer.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public static class DatabasesMigrator
    {
        public async static Task MigrateIdentityConfigurationAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            await configurationDbContext.Database.MigrateAsync();
        }

        public async static Task MigrateIdentityOperationalAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();

            await persistedGrantDbContext.Database.MigrateAsync();
        }

        public async static Task MigrateUsersDatabaseAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await applicationDbContext.Database.MigrateAsync();
        }
    }
}
