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
        [HttpGet("login")]
        public async Task<IActionResult> Login(User user)
        {
            if (!await _useCase.LoginUser(user))
            {
                return Unauthorized("Invalid Password");
            }

            return Ok("Login completed successfully");
        }
    }
}
