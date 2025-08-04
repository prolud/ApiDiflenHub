using System.Net;
using Application.Auth;
using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;

namespace Application.UseCases
{
    public class LoginUseCase(IUserRepository userRepository, JwtService jwtService)
    {
        public async Task<UseCaseResult> ExecuteAsync(string email, string password)
        {
            var userFromDatabase = await userRepository.GetAsync(u => u.Email == email);

            if (userFromDatabase is null) return new()
            {
                Content = "Usuário não encontrado",
                StatusCode = HttpStatusCode.Unauthorized
            };

            else if (!BCrypt.Net.BCrypt.Verify(password, userFromDatabase.Password))
            {
                return new()
                {
                    Content = "Senha incorreta",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            return new()
            {
                Content = new LoginDtoOut()
                {
                    IsLogged = true,
                    AccessToken = jwtService.GenerateBearerToken(userFromDatabase),
                    ExpiresIn = jwtService.GetExpirationDate(),
                    Message = "Successfully logged."
                }
            };
        }
    }
}