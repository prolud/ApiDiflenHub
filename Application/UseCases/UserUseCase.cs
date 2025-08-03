using Application.Auth;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Application.UseCases
{
    public class UserUseCase(IUserService _userService, JwtService _jwtService)
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
    }
}