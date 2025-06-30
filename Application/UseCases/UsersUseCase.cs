using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class UsersUseCase(IUserService _userService)
    {
        public async Task RegisterUser(User user) => await _userService.InsertUser(user);

        public async Task<bool> LoginUser(User user) => await _userService.IsValidPassword(user.Username, user.Password);
    }
}