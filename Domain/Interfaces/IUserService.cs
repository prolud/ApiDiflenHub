using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        public Task InsertUser(User user);
        public Task<bool> IsValidPassword(string username, string password);
    }
}