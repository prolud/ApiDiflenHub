using Microsoft.AspNetCore.Mvc;

namespace ApiDiflenStore.Controllers
{
    public class LogginRequestBody
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("say-hello")]
        public IActionResult SayHello()
        {
            return Ok("Hello, World!");
        }
    }
}
