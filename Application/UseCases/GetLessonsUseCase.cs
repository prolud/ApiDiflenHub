using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLessonsUseCase(IUnityRepository unityRepository, ILessonRepository lessonRepository, ILessonService lessonService)
    {
        public async Task<UseCaseResult<List<LessonDtoOut>>> ExecuteAsync(string unityName, string userId)
        {
            var result = new UseCaseResult<List<LessonDtoOut>>();
            
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new()
            {
                Content = [],
                Message = "Nenhuma unidade foi encontrada."
            };

            var lessons = new List<LessonDtoOut>();
            var dblessons = await lessonRepository.GetListAsync(l => l.UnityId == unity.Id);

            foreach (var lesson in dblessons)
            {
                lessons.Add(new LessonDtoOut()
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    VideoUrl = lesson.VideoUrl,
                    Concluded = await lessonService.LessonAreAlreadyAnswered(userId, lesson.Id),
                });
            }

            return result;
        }
    }
}