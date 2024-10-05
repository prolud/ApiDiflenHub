using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDiflenStore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("say-hello")]
        public ActionResult<List<Categorie>> SayHello([FromServices] AppDbContext appDbContext)
        {
            var categories = appDbContext.Categorie
                .AsNoTracking()
                .ToList();

            return Ok(categories);
        }
    }
}
