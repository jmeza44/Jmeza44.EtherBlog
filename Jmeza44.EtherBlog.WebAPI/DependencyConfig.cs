using FluentValidation.AspNetCore;
using Jmeza44.EtherBlog.Application.Common.AppSettingsMapping;
using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Jmeza44.EtherBlog.WebApi.Common.Authentication;
using Jmeza44.EtherBlog.WebApi.Common.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;

namespace Jmeza44.EtherBlog.WebApi
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddConfigurationMapping(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config.GetSection("App"));
            services.Configure<AuthenticationOptions>(config.GetSection("App:AuthenticationOptions"));

            return services;
        }

        public static IServiceCollection AddAspNetServices(this IServiceCollection services, IConfiguration config)
        {
            string[] allowedOrigins = config.GetSection("App:CorsPolicy:AllowedOrigins").Get<string[]>() ?? throw new AppSettingMissingException("App:CorsPolicy:AllowedOrigins");
            string policyName = config.GetValue<string>("App:CorsPolicy:Name") ?? throw new AppSettingMissingException("App:CorsPolicy:Name");

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder =>
                    {
                        builder.WithOrigins(allowedOrigins)
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            AuthenticationOptions authenticationOptions = BindAuthenticationOptions(config);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = authenticationOptions.Authority;
                        options.Audience = authenticationOptions.Audience;
                        options.TokenValidationParameters.ValidTypes = authenticationOptions.TokenValidationParameters.ValidTypes;
                    });

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }

        public static IServiceCollection AddFluentValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            return services;
        }

        public static IServiceCollection AddInMemoryCache(this IServiceCollection services, IConfiguration config)
        {
            services.AddMemoryCache(options =>
            {
                options.SizeLimit = config.GetValue<long?>("App:InMemoryCacheConfiguration:SizeLimitUnits");
                options.TrackStatistics = config.GetValue<bool?>("App:InMemoryCacheConfiguration:TrackStatistics") ?? false;
            });

            return services;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            var policyName = app.Configuration.GetValue<string>("App:CorsPolicy:Name") ?? throw new AppSettingMissingException("App:CorsPolicy:Name");

            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(policyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.MapControllers();

            return app;
        }

        private static AuthenticationOptions BindAuthenticationOptions(IConfiguration config)
        {
            var authenticationOptions = new AuthenticationOptions()
            {
                Audience = "",
                Authority = "",
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidTypes = []
                }
            };
            config.GetSection("App:AuthenticationOptions").Bind(authenticationOptions);

            if (string.IsNullOrWhiteSpace(authenticationOptions.Authority))
                throw new AppSettingMissingException("App:AuthenticationOptions:Authority");

            if (string.IsNullOrWhiteSpace(authenticationOptions.Audience))
                throw new AppSettingMissingException("App:AuthenticationOptions:Audience");

            if (authenticationOptions.TokenValidationParameters == null || authenticationOptions.TokenValidationParameters.ValidTypes == null)
                throw new AppSettingMissingException("App:AuthenticationOptions:TokenValidationParameters:ValidTypes");
            return authenticationOptions;
        }
    }
}
