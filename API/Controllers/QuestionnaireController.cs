using System.Net;
using System.Security.Claims;
using Application.UseCases;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/questionnaire")]
    [Authorize]
    public class QuestionnaireController(QuestionnaireUseCase _useCase) : ControllerBase
    {
        /// <summary>
        /// Veryfy answer s
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="unityName"></param>
        /// <param name="answers"></param>
        /// <returns></returns>
        [HttpPost("verify-answers")]
        public async Task<IActionResult> VerifyAnswers([FromBody] List<AnswerVerifyIn> answersVerifyIn)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var lessonId = await _useCase.VerifyAnswersAsync(answersVerifyIn, userId);

            if (lessonId is null)
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Não foi possível encontrar uma das alternativas de alguma questão",
                });
            }

            return Ok(await _useCase.GetLastAnswersAsync((int)lessonId, userId));
        }

        [HttpGet("get-last-answers")]
        public async Task<IActionResult> GetAnswers([FromQuery] int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || userId == "0") return Unauthorized();

            return Ok(await _useCase.GetLastAnswersAsync(lessonId, userId));
        }
    }
}