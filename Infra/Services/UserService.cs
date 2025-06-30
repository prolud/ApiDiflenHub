using Domain.Interfaces;
using Domain.Models;

namespace Infra.Services
{
    public class UserService(AppDbContext _context) : IUserService
    {
        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsValidPassword(string username, string password)
        {
            return false;
        }
    }
}