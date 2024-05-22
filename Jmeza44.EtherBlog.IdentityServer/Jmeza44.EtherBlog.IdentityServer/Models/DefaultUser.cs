namespace IdentityServer.Models
{
    public class DefaultUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public string Password { get; set; }
    }
}
