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

        public LoginResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty");
            
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                Console.WriteLine($"User not found: {username}");
                throw new ArgumentException("Invalid username or password");
            }

            if (!PasswordHelper.VerifyPassword(password, user.Password))
            {
                Console.WriteLine($"Password verification failed for user: {username}");
                throw new ArgumentException("Invalid username or password");
            }

            Console.WriteLine($"User found and password verified for: {username}");
            var token = JwtHelper.GenerateToken(user.Id, user.Role, _config["JwtSettings:SecretKey"]);
            
            return new LoginResult
            {
                Token = token,
                Role = user.Role
            };
        }
    }
}
