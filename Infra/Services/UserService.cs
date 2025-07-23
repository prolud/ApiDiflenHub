using System.Formats.Asn1;
using Domain.Enums;
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

        public async Task<User?> GetUserAsync(string queryParam, QueryParam queryParamEnum)
        {
            switch (queryParamEnum)
            {
                case QueryParam.Email:
                    return await _context.Users.FirstOrDefaultAsync(user => user.Email == queryParam);
                case QueryParam.UserName:
                    return await _context.Users.FirstOrDefaultAsync(user => user.Username == queryParam);
                default:
                    return null;
            }
        }

        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}