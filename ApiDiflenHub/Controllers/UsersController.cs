using DbContext;
using Microsoft.AspNetCore.Mvc;

namespace ApiDiflenStore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Class1 _class1;

        public UsersController(Class1 class1)
        {
            _class1 = class1;
        }

        [HttpGet("say-hello")]
        public IActionResult SayHello()
        {
            return Ok(_class1.ReturnHello());
        }
    }
}
