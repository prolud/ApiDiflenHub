using Application.UseCases;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/questionnaire")]
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
            if (answersVerifyIn.Any(a => a.AlternativeId <= 0) || answersVerifyIn.Any(a => a.QuestionId <= 0))
            {
                return BadRequest("Requisição inválida. Informe o ID da questão eo ID da alternativa.");
            }

            var answersVerifyOut = await _useCase.VerifyAnswersAsync(answersVerifyIn);

            if (answersVerifyOut is null)
            {
                return BadRequest("Não foi possível encontrar uma das alternativas de alguma questão.");
            }

            return Ok(answersVerifyOut);
        }
    }
}