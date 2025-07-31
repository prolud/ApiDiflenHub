using Application.Auth;
using Domain.DTOs;
using Domain.Enums;
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

        public async Task<LoginDtoOut?> LoginUser(string email, string password)
        {
            var userFromDatabase = await _userService.GetUserAsync(email, QueryParam.Email);

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

        public async Task<ProfileDtoOut?> GetProfileAsync(string username)
        {
            var userFromDatabase = await _userService.GetUserAsync(username, QueryParam.UserName);
            if (userFromDatabase is null) return null;

            return new ProfileDtoOut
            {
                Id = userFromDatabase.Id,
                Experience = userFromDatabase.Experience,
                Username = userFromDatabase.Username,
                ProfilePic = $"data:{userFromDatabase.FileType};base64,{System.Text.Encoding.UTF8.GetString(userFromDatabase.ProfilePicture)}",
            };
        }
    }
}