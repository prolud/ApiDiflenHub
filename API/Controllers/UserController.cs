using System.Net;
using Application.UseCases;
using Domain.DTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController(UserUseCase _useCase) : ControllerBase
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDtoIn registerDto)
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
        public async Task<IActionResult> Login(LoginDtoIn loginDto)
        {
            var result = await _useCase.LoginUser(loginDto.Email, loginDto.Password);

            if (result is null)
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Usuário não encontrado",
                });
            }
            else if (!result.IsLogged)
            {
                return Unauthorized(new
                {
                    HttpStatusCode.Unauthorized,
                    Message = "Senha incorreta",
                });
            }

            return Ok(result);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile([FromQuery] string username)
        {
            var profile = await _useCase.GetProfileAsync(username);

            if (profile is null) return NoContent();

            return Ok(profile);
        }
    }
}