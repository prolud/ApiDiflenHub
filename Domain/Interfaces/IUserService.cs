using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        public Task InsertUser(User user);
        public Task<User?> GetUser(string email);
    }
}