using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        public Task InsertUser(User user);
        public Task<User?> GetUserAsync(string queryParam, QueryParam queryParamEnum);
        public Task AddExperience(int experienceToAdd, int userId);
    }
}