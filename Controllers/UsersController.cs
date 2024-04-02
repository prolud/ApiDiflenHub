using ApiDiflenStore.Db;
using ApiDiflenStore.Db.Models;
using ApiDiflenStore.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDiflenStore.Controllers
{
    public class LogginReturn()
    {
        public int? IdUsers { get; set; }
        public bool SuccessStatus { get; set; }
        public string Reason { get; set; }
        public string? Username { get; set; }
        public UserRolesEnum.IdEnum? UserRole { get; set; }
    }

    public class LogginRequestBody
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("loggin")]
        public async Task<IActionResult> Loggin([FromBody] LogginRequestBody logginRequestBody)
        {
            try
            {
                var user = _appDbContext.Users.Where(_ => _.Email == logginRequestBody.Email && _.Password == logginRequestBody.Password).FirstOrDefault();

                var logginRegurn = new LogginReturn()
                {
                    IdUsers = user != null ? user.IdUsers : null,
                    SuccessStatus = user != null,
                    Reason = user != null ? "Login completed successfully." : "Email or password wrong.",
                    Username = user != null ? user.Email.Split("@")[0] : null,
                    UserRole = user != null ? user.IdUserRoles : null,
                };

                if (user != null)
                {
                    user.IsLogged = true;
                    await _appDbContext.SaveChangesAsync();
                }

                return Ok(logginRegurn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("loggout")]
        public async Task<IActionResult> Loggout(string email, string password)
        {
            try
            {
                var user = _appDbContext.Users.Where(_ => _.Email == email && _.Password == password).First();
                user.IsLogged = false;
                await _appDbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(string email, string password, UserRolesEnum.IdEnum userLevel)
        {
            try
            {
                var user = new Users()
                {
                    Email = email,
                    Password = password,
                    IdUserRoles = userLevel,
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

                if (user == null)
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
