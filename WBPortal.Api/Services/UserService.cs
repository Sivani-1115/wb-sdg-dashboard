using WBPortal.Api.Data;
using WBPortal.Api.Models;

namespace WBPortal.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers() => _context.Users.ToList();
        public User GetUserById(int id) => _context.Users.Find(id);
        public void CreateUser(User user)
        {
            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            if (!string.IsNullOrEmpty(user.Password))
                user.Password = PasswordHelper.HashPassword(user.Password);

            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
