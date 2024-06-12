using IdentityModel.Client;
using Jmeza44.EtherBlog.Application.Common.AppSettingsMapping;
using Jmeza44.EtherBlog.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace Jmeza44.EtherBlog.WebApi.Common.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationOptions _authenticationOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrentUserService(IOptions<AuthenticationOptions> authenticationOptions, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _authenticationOptions = authenticationOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string?> GetCurrentUserEmailAsync()
        {
            var authorityUrl = _authenticationOptions.Authority;
            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new ArgumentNullException(nameof(_httpContextAccessor.HttpContext));

            var bearerTokenFromRequestHeaders = httpContext.Request.Headers["Authorization"].ToString();

            if (bearerTokenFromRequestHeaders == null)
            {
                return null;
            }

            if (!bearerTokenFromRequestHeaders.StartsWith("Bearer "))
            {
                return null;
            }

            bearerTokenFromRequestHeaders = bearerTokenFromRequestHeaders[7..]; // Starts with "Bearer ", that part is removed

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = $"{authorityUrl}/connect/userinfo",
                Token = bearerTokenFromRequestHeaders
            });
            if (response.IsError) throw new Exception(response.Error);

            var claims = response.Claims;

            var identifierClaim = claims.FirstOrDefault(c => c.Type == "email") ?? claims.FirstOrDefault(c => c.Type == "sub");

            return identifierClaim?.Value ?? null;
        }


        public bool IsInRole(string roleName) =>
            _httpContextAccessor!.HttpContext!.User.IsInRole(roleName);
    }
}
