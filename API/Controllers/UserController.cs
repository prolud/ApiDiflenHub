using System.Net;
using Application.UseCases;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, LoginUseCase loginUseCase, UserUseCase _useCase) : ControllerBase
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
            var result = await loginUseCase.ExecuteAsync(loginDto.Email, loginDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile([FromQuery] string username)
        {
            var user = await userRepository.GetAsync(u => u.Username == username);

            if (user is null) return NoContent();

            return Ok(
                new ProfileDtoOut
                {
                    Id = user.Id,
                    Experience = user.Experience,
                    Username = user.Username,
                    ProfilePic = $"data:{user.FileType};base64,{System.Text.Encoding.UTF8.GetString(user.ProfilePicture)}",
                });
        }
    }
}