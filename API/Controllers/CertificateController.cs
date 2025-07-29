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

    public class CertificateController(CertificateUseCase useCase) : ControllerBase
    {
        [HttpPost("issue")]
        public async Task<IActionResult> IssueNewCertificate([FromQuery] string unityName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var result = await useCase.IssueNewCertificate(userId, unityName);

            if (result is null)
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Não foi possível emitir o certificado. Certifique-se de que a unidade solicitada realmente existe."
                });
            }

            if ((bool)!result)
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Não foi possível emitir o certificado. Certifique-se de que todas as questões foram respondidas ou que o certificado já não tenha sido emitido."
                });
            }

            return Ok("Certificado emitido com sucesso!");
        }
    }
}