using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace IdentityServer.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed roles
            var rolesToSeed = new[] { "Admin", "Editor", "Viewer" };
            foreach (var role in rolesToSeed)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    Log.Debug($"Role '{role}' created");
                }
                else
                {
                    Log.Debug($"Role '{role}' already exists");
                }
            }
        }

        public static async Task SeedUsersAsync(WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            using var scope = app.Services.CreateScope();

            var defaultUsers = app.Configuration.GetRequiredSection("App:DefaultUsers").Get<DefaultUser[]>();
            foreach (var defaultUser in defaultUsers)
            {
                var user = new ApplicationUser { UserName = defaultUser.UserName, Email = defaultUser.Email, EmailConfirmed = true };
                await SeedUserAsync(scope, user, defaultUser.Password, defaultUser.RoleName);
            }
        }

        private static async Task SeedUserAsync(IServiceScope scope, ApplicationUser userToAdd, string password, string roleName)
        {
            ArgumentNullException.ThrowIfNull(scope);
            ArgumentNullException.ThrowIfNull(userToAdd);
            ArgumentNullException.ThrowIfNull(userToAdd.UserName);
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(roleName);

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var userFound = await userManager.FindByNameAsync(userToAdd.UserName);
            if (userFound == null)
            {
                var result = await userManager.CreateAsync(userToAdd, password);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug($"{userToAdd.UserName} created");

                await userManager.AddToRoleAsync(userToAdd, roleName);
            }
            else
            {
                Log.Debug($"{userToAdd.UserName} already exists");

                await userManager.AddToRoleAsync(userFound, roleName);
            }

        }

        public async static Task SeedIdentityConfigurationAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            var apiResources = app.Configuration.GetRequiredSection("App:IdentityConfiguration:ApiResources").Get<ApiResource[]>();
            foreach (var apiResource in apiResources)
            {
                var exists = configurationDbContext.ApiResources.FirstOrDefault(ar => ar.Name == apiResource.Name) != null;
                if (exists) continue;
                configurationDbContext.ApiResources.Add(apiResource);
                await configurationDbContext.SaveChangesAsync();
            }

            var apiScopes = app.Configuration.GetRequiredSection("App:IdentityConfiguration:ApiScopes").Get<ApiScope[]>();
            foreach (var apiScope in apiScopes)
            {
                var exists = configurationDbContext.ApiScopes.FirstOrDefault(asc => asc.Name == apiScope.Name) != null;
                if (exists) continue;
                configurationDbContext.ApiScopes.Add(apiScope);
                await configurationDbContext.SaveChangesAsync();
            }

            var clients = app.Configuration.GetRequiredSection("App:IdentityConfiguration:Clients").Get<Client[]>();
            foreach (var client in clients)
            {
                var exists = configurationDbContext.Clients.FirstOrDefault(c => c.ClientId == client.ClientId) != null;
                if (exists) continue;
                if (client.ClientSecrets != null)
                {
                    foreach (var secret in client.ClientSecrets)
                    {
                        secret.Value = secret.Value.ToSha256();
                    }
                }
                configurationDbContext.Clients.Add(client);
                await configurationDbContext.SaveChangesAsync();
            }

            var identityResources = app.Configuration.GetRequiredSection("App:IdentityConfiguration:IdentityResources").Get<IdentityResource[]>();
            foreach (var identityResource in identityResources)
            {
                var exists = configurationDbContext.IdentityResources.FirstOrDefault(ir => ir.Name == identityResource.Name) != null;
                if (exists) continue;
                configurationDbContext.IdentityResources.Add(identityResource);
                await configurationDbContext.SaveChangesAsync();
            }
        }
    }
}