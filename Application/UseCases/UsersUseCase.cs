using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class UsersUseCase(IUserService _userService)
    {
        public async Task RegisterUser(string email, string username, string password)
        {
            var user = new User()
            {
                Email = email,
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _userService.InsertUser(user);
        }

        public async Task<bool?> LoginUser(string email, string password)
        {
            var userFromDatabase = await _userService.GetUser(email);

            if (userFromDatabase is null) return null;
            else
            {
                return BCrypt.Net.BCrypt.Verify(password, userFromDatabase.Password);
            }
        }
    }
}