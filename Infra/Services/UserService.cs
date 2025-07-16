using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class UserService(AppDbContext _context) : IUserService
    {
        public async Task AddExperience(int experienceToAdd, int userId)
        {
            var user = _context.Users.First(u => u.Id == userId);
            user.Experience += experienceToAdd;

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUser(string email) => await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}