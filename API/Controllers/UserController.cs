using API.DTOs;
using Application.UseCases;
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
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _useCase.RegisterUser(registerDto.Email, registerDto.Username, registerDto.Password);
            return Ok("User created successfully!");
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _useCase.LoginUser(loginDto.Email, loginDto.Password);
            
            if (result is null)
            {
                return BadRequest("Usuário não encontrado");
            }
            else if (result is false)
            {
                return Unauthorized("Senha incorreta");
            }

            return Ok("Login completed successfully");
        }
    }
}
