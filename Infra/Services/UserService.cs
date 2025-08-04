using Domain.Interfaces.Services;

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
    }
}