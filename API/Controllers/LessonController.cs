using System.Security.Claims;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    [Authorize]
    public class LessonController(GetLessonsUseCase getLessonsUseCase, GetLessonUseCase getLessonUseCase) : ControllerBase
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
            var result = await getLessonsUseCase.ExecuteAsync(unityName, userId);

            return StatusCode((int)result.StatusCode, result.Content);
        }

        [HttpGet("get-lesson")]
        public async Task<IActionResult> GetLesson([FromQuery] string unityName, [FromQuery] int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await getLessonUseCase.ExecuteAsync(unityName, lessonId, userId);

            return StatusCode((int)result.StatusCode, result.Content);
        }
    }
}
