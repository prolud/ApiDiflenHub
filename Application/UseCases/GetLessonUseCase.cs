using System.Net;
using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLessonUseCase(IUnityRepository unityRepository, ILessonRepository lessonRepository, ILessonService lessonService)
    {
        public async Task<UseCaseResult> ExecuteAsync(string unityName, int lessonId, string userId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new()
            {
                StatusCode = HttpStatusCode.NoContent
            };

            var lessonFromDb = await lessonRepository.GetLesson(lessonId);
            if (lessonFromDb is null) return new()
            {
                StatusCode = HttpStatusCode.NoContent
            };

            var questions = lessonFromDb.Questions
            .Select(q => new QuestionDtoOut
            {
                Id = q.Id,
                Statement = q.Statement,
                Alternatives = q.Alternatives
                .Select(a => new AlternativeDtoOut
                {
                    Id = a.Id,
                    QuestionId = a.QuestionId,
                    Text = a.Text
                })
                .ToList()
            })
            .ToList();

            return new()
            {
                Content = new LessonDtoOut()
                {
                    Id = lessonFromDb.Id,
                    Description = lessonFromDb.Description,
                    Title = lessonFromDb.Title,
                    VideoUrl = lessonFromDb.VideoUrl,
                    Questions = questions,
                    Concluded = await lessonService.LessonAreAlreadyAnswered(userId, lessonFromDb.Id),
                },
            };
        }
    }
}