using System.Net;
using System.Security.Claims;
using Application.UseCases;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    [Authorize]
    public class LessonController(LessonUseCase _useCase) : ControllerBase
    {
        /// <summary>
        /// Lesson
        /// </summary>
        /// <param name="unityId"></param>
        /// <returns></returns>
        [HttpGet("get-lessons-by-unity-name")]
        public async Task<IActionResult> GetLessonsFromUnity([FromQuery] string unityName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            List<LessonDtoOut> lessons;

            if (!string.IsNullOrEmpty(unityName))
            {
                lessons = await _useCase.GetLessonsByUnityName(unityName, userId);
            }
            else
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Endereço inválido. Informe o nome da unidade.",
                });
            }

            if (lessons.Count == 0)
            {
                return NoContent();
            }

            return Ok(lessons);
        }

        [HttpGet("get-lesson")]
        public async Task<IActionResult> GetLesson([FromQuery] string unityName, [FromQuery] int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            if (lessonId >= 0 && !string.IsNullOrEmpty(unityName))
            {
                var lesson = await _useCase.GetLesson(unityName, lessonId, userId);

                if (lesson is null)
                {
                    return NoContent();
                }

                return Ok(lesson);
            }

            return BadRequest(new
            {
                HttpStatusCode.BadRequest,
                Message = "Endereço inválido. Informe o índice da lição e o Id da unidade.",
            });
        }
    }
}
