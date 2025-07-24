using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/certificate")]
    [ApiController]
    [Authorize]

    public class CertificateController : ControllerBase
    {
        [HttpPost("issue")]
        public async Task<IActionResult> IssueNewCertificate()
        {
            return Ok();
        }
    }
}