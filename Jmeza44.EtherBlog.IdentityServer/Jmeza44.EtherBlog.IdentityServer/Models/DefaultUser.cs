namespace IdentityServer.Models
{
    public class DefaultUser
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required bool EmailConfirmed { get; set; } = true;
        public required string Password { get; set; }
        public required string RoleName { get; set; }
    }
}
