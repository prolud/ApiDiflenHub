using ApiDiflenStore.Db;
using ApiDiflenStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _appDbContext.Users.ToList();
            return Ok(users);
        }
    }
}
