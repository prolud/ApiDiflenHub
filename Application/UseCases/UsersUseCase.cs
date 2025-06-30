using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class UsersUseCase(IUserService _userService)
    {
        public async Task RegisterUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userService.InsertUser(user);
        }

        public async Task<bool?> LoginUser(User user)
        {
            var userFromDatabase = await _userService.GetUser(user.Email);

            if (userFromDatabase is null) return null;
            else
            {
                return BCrypt.Net.BCrypt.Verify(user.Password, userFromDatabase.Password);
            }
        }
    }
}