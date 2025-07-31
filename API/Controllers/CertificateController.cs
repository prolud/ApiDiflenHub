using System.Net;
using System.Security.Claims;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/certificate")]
    [ApiController]
    [Authorize]

    public class CertificateController(IssueCertificateUseCase issueCertificateUseCase) : ControllerBase
    {
        [HttpPost("issue")]
        public async Task<IActionResult> IssueNewCertificate([FromQuery] string unityName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await issueCertificateUseCase.ExecuteAsync(userId, unityName);

            return StatusCode((int)result.StatusCode, result.Content);
        }
    }
}