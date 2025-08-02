using System.Security.Claims;
using Application.UseCases;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/questionnaire")]
    [ApiController]
    [Authorize]
    public class QuestionnaireController(VerifyAnswersUseCase verifyAnswersUseCase, GetLastAnswersUseCase getLastAnswersUseCase) : ControllerBase
    {
        /// <summary>
        /// Veryfy answers
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="unityName"></param>
        /// <param name="answers"></param>
        /// <returns></returns>
        [HttpPost("verify-answers")]
        public async Task<IActionResult> VerifyAnswers([FromBody] AnswerVerifyIn answerVerifyIn)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value!; 
            var result = await verifyAnswersUseCase.ExecuteAsync(answerVerifyIn, userId);

            if (result.IsSuccessStatusCode)
            {
                return StatusCode((int)result.StatusCode, result.Content);
            }

            return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpGet("get-last-answers")]
        public async Task<IActionResult> GetAnswers([FromQuery] int lessonId, [FromQuery] string unityName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await getLastAnswersUseCase.ExecuteAsync(userId, lessonId, unityName);

            return Ok(result);
        }
    }
}