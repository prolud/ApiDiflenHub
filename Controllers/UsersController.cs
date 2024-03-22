using ApiDiflenStore.Db;
using ApiDiflenStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDiflenStore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> CreateUser(string email, string password, int userLevel)
        {
            try
            {
                var user = new Users()
                {
                    Email = email,
                    Password = password,
                    UserLevel = userLevel,
                    CreationDate = DateTime.Now
                };

                if (_appDbContext.Users.Where(_ => _.Email.ToLower() == user.Email).Any())
                {
                    return BadRequest("Email already exists.");
                }
                else
                {
                    _appDbContext.Users.Add(user);
                    await _appDbContext.SaveChangesAsync();
                    return Ok(user);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string email, string password)
        {
            try
            {
                await _appDbContext.Users
                    .Where(_ => _.Email == email && _.Password == password)
                    .ExecuteDeleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                var user = _appDbContext.Users
                    .Where(_ => _.Email == email && _.Password == oldPassword)
                    .FirstOrDefault();

                if(user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                user.Password = newPassword;
                await _appDbContext.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
