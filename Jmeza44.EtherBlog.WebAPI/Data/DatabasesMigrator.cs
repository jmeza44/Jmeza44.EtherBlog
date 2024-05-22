using Jmeza44.EtherBlog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.WebApi.Data
{
    public static class DatabasesMigrator
    {

        public async static Task MigrateApplicationDatabaseAsync(this WebApplication app)
        {
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("Starting migration of application database...");

            try
            {
                using var scope = app.Services.CreateScope();
                var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await appDbContext.Database.MigrateAsync();
                logger.LogInformation("Migration of application database completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migration of application database failed.");
                throw;
            }
        }
    }
}
