using Application.Auth;
using Domain.DTOs;
using Domain.Interfaces;
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

        public async Task<LoginDtoOut?> LoginUser(string email, string password)
        {
            var userFromDatabase = await _userService.GetUser(email);

            if (userFromDatabase is null) return null;

            else if (!BCrypt.Net.BCrypt.Verify(password, userFromDatabase.Password))
            {
                return new LoginDtoOut();
            }

            return new LoginDtoOut
            {
                IsLogged = true,
                AccessToken = _jwtService.GenerateBearerToken(userFromDatabase),
                ExpiresIn = _jwtService.GetExpirationDate(),
                Message = "Successfully logged."
            };
        }
    }
}