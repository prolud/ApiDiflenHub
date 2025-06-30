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
        public async Task<ActionResult> Register(User user)
        {
            await _useCase.RegisterUser(user);
            return Ok("User created successfully!");
        }
    }
}
