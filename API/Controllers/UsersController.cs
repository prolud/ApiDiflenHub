using Application.UseCases;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDiflenStore.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController(UsersUseCase _useCase) : ControllerBase
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("register")]
        public async Task<IActionResult> Register(User user)
        {
            await _useCase.RegisterUser(user);
            return Ok("User created successfully!");
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            var result = await _useCase.LoginUser(user);
            
            if (result is null)
            {
                return BadRequest("Usuário não encontrado");
            }
            else if (result is false)
            {
                return Unauthorized("Invalid Password");
            }

            return Ok("Login completed successfully");
        }
    }
}
