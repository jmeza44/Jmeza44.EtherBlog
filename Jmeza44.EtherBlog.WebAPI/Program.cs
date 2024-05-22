using Jmeza44.EtherBlog.Application;
using Jmeza44.EtherBlog.Infrastructure;
using Jmeza44.EtherBlog.WebApi;
using Jmeza44.EtherBlog.WebApi.Data;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAspNetServices(builder.Configuration)
                        .AddApplication()
                        .AddInfrastructure(builder.Configuration)
                        .AddFluentValidations()
                        .AddSwagger()
                        .AddAuthenticationServices(builder.Configuration)
                        .AddInMemoryCache(builder.Configuration);

        var app = builder.Build();

        app.ConfigurePipeline();

        await app.MigrateApplicationDatabaseAsync();

        app.Run();
    }
}