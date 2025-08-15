using System.Security.Claims;
using Application.UseCases;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/certificate")]
    [ApiController]
    [Authorize]

    public class CertificateController(
        IssueCertificateUseCase issueCertificateUseCase,
        ICertificateRepository certificateRepository) : ControllerBase
    {
        [HttpPost("issue")]
        public async Task<IActionResult> IssueNewCertificate([FromQuery] string unityName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await issueCertificateUseCase.ExecuteAsync(userId, unityName);

            return StatusCode((int)result.StatusCode, result.Content);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetUserCertificates()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            {
                return BadRequest("Invalid user ID");
            }

            var certificates = await certificateRepository.GetCertificatesByUserId(userIdInt);

            return Ok(certificates.Select(c => new
            {
                UnityName = c.Unity.Name,
                c.CreatedAt
            })
            .ToList());
        }
    }
}