using WBPortal.Api.Models;

namespace WBPortal.Api.Services
{
    public interface IAuthService
    {
        void Register(User user);
        LoginResult Login(string username, string password);
    }

    public class LoginResult
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
