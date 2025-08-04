using Application.UseCases.Common;
using Domain.Interfaces.Repositories;
using Domain.Models;
using static BCrypt.Net.BCrypt;

namespace Application.UseCases
{
    public class RegisterUseCase(IUserRepository userRepository)
    {
        public async Task<UseCaseResult> ExecuteAsync(string email, string username, string password)
        {
            var user = new User()
            {
                Email = email,
                Username = username,
                Password = HashPassword(password)
            };

            await userRepository.InsertAsync(user);

            return new()
            {
                Content = "Usuário criado com sucesso"
            };
        }
    }
}