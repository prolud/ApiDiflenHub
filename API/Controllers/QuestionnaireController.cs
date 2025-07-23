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
            if (_useCase.TheresMoreThanOneLessonId(answersVerifyIn))
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Por favor, envie questões de apenas uma aula por vez",
                });
            }

            var lessonId = answersVerifyIn.First().LessonId;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (await _useCase.QuestionsAreAlreadyAnswered(userId, lessonId))
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Todas as questões já foram respondidas",
                });
            }

            var result = await _useCase.VerifyAnswersAsync(answersVerifyIn, userId);
            if (result is null)
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Não foi possível encontrar uma das alternativas de alguma questão",
                });
            }

            return Ok(result);
        }

        [HttpGet("get-last-answers")]
        public async Task<IActionResult> GetAnswers([FromQuery] int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || userId == "0") return Unauthorized();

            return Ok(await _useCase.GetLastAnswersAsync(userId, lessonId));
        }
    }
}