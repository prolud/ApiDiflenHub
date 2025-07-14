using System.Net;
using Application.UseCases;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lessons")]
    [Authorize]
    public class LessonController(LessonUseCase _useCase) : ControllerBase
    {
        /// <summary>
        /// Lesson
        /// </summary>
        /// <param name="unityId"></param>
        /// <returns></returns>
        [HttpGet("get-lessons-by-unity")]
        public async Task<IActionResult> GetLessonsFromUnity([FromQuery] int unityId, [FromQuery] string unityName)
        {
            List<LessonDtoOut> lessons;

            if (unityId > 0)
            {
                lessons = await _useCase.GetLessonsByUnityId(unityId);
            }
            else if (!string.IsNullOrEmpty(unityName))
            {
                lessons = await _useCase.GetLessonsByUnityName(unityName);
            }
            else
            {
                return BadRequest(new
                {
                    HttpStatusCode.BadRequest,
                    Message = "Endereço inválido. Informe o ID ou o nome da unidade.",
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
            if (lessonId >= 0 && !string.IsNullOrEmpty(unityName))
            {
                var lesson = await _useCase.GetLesson(unityName, lessonId);

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
