using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task AddExperience(int experienceToAdd, int userId)
        {
            var user = context.Users.First(u => u.Id == userId);
            user.Experience += experienceToAdd;

            context.Update(user);
            await context.SaveChangesAsync();
        }
    }
}