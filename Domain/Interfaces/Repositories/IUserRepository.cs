using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task AddExperience(int experienceToAdd, int userId);
    }
}