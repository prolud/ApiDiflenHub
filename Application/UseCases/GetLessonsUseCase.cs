using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLessonsUseCase(IUnityRepository unityRepository, ILessonRepository lessonRepository, ILessonService lessonService)
    {
        public async Task<UseCaseResult<LessonDtoOut[]>> ExecuteAsync(string unityName, string userId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new()
            {
                Content = [],
                Message = "Nenhuma unidade foi encontrada."
            };

            var dblessons = await lessonRepository.GetListAsync(l => l.UnityId == unity.Id);

            var lessons = await Task.WhenAll(
                dblessons.Select(async lesson => new LessonDtoOut
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    VideoUrl = lesson.VideoUrl,
                    Concluded = await lessonService.LessonAreAlreadyAnswered(userId, lesson.Id),
                })
                .ToList());

            return new()
            {
                Content = lessons,
            };
        }
    }
}