using IdentityServer.Data;
using IdentityServer.Startup;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
           .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}").Enrich
           .FromLogContext().ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    Log.Information("Migrating Identity Configuration entities...");
    await DatabasesMigrator.MigrateIdentityConfigurationAsync(app);
    Log.Information("Done Migrating Identity Configuration entities. Exiting.");

    Log.Information("Seeding Identity Server Configuration...");
    await SeedData.SeedIdentityConfigurationAsync(app);
    Log.Information("Done Seeding Identity Server Configuration. Exiting.");

    Log.Information("Migrating Identity Operational entities...");
    await DatabasesMigrator.MigrateIdentityOperationalAsync(app);
    Log.Information("Done Migrating Identity Operational entities. Exiting.");

    Log.Information("Migrating Users databases...");
    await DatabasesMigrator.MigrateUsersDatabaseAsync(app);
    Log.Information("Done Migrating Users databases. Exiting.");

    Log.Information("Seeding Roles databases...");
    await SeedData.SeedRolesAsync(app);
    Log.Information("Done seeding Roles databases. Exiting.");

    Log.Information("Seeding Users databases...");
    await SeedData.SeedUsersAsync(app);
    Log.Information("Done seeding Users databases. Exiting.");

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}