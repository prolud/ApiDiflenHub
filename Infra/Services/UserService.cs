using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class UserService(AppDbContext _context) : IUserService
    {
        public async Task<User?> GetUser(string email) => await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}