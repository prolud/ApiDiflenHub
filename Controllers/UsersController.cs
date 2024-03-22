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
        public async Task<IActionResult> CreateUser(string username, string password, int userLevel)
        {
            try
            {
                var user = new Users()
                {
                    Username = username,
                    Password = password,
                    UserLevel = userLevel,
                    CreationDate = DateTime.Now
                };

                _appDbContext.Users.Add(user);
                await _appDbContext.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _appDbContext.Users.ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string username, string password)
        {
            try
            {
                await _appDbContext.Users.Where(_ => _.Username == username && _.Password == password).ExecuteDeleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(string oldUsername, string oldPassword, string newUsername, string newPassword)
        {
            try
            {
                var user = _appDbContext.Users
                    .Where(_ => _.Username == oldUsername && _.Password == oldPassword)
                    .First();

                user.Username = newUsername;
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
