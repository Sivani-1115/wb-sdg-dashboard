using WBPortal.Api.Models;

namespace WBPortal.Api.Services
{
    public interface IAuthService
    {
        void Register(User user);
        string Login(string username, string password);
    }
}
