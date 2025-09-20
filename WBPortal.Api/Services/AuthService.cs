using WBPortal.Api.Data;
using WBPortal.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WBPortal.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public void Register(User user)
        {
            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public string Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
                throw new Exception("Invalid credentials");

            return JwtHelper.GenerateToken(user.Id, user.Role, _config["JwtSettings:SecretKey"]);
        }
    }
}
