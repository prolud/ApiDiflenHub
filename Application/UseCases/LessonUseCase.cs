using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases;

public class LessonUseCase(ILessonService _lessonService, IUnityService _unityService, QuestionnaireUseCase _questionnaireUseCase)
{
    public async Task<List<LessonDtoOut>> GetLessonsByUnityId(int unityId, string userId)
    {
        var lessons = new List<LessonDtoOut>();
        var dblessons = await _lessonService.GetLessonsFromUnityId(unityId);

        foreach (var lesson in dblessons)
        {
            lessons.Add(new LessonDtoOut()
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                VideoUrl = lesson.VideoUrl,
                Concluded = await _questionnaireUseCase.QuestionsAreAlreadyAnswered(userId, lesson.Id),
            });
        }

        return lessons;
    }

    public async Task<List<LessonDtoOut>> GetLessonsByUnityName(string unityName, string userId)
    {
        var unity = await _unityService.GetUnityByName(unityName);

        if (unity is null) return [];

        var lessons = new List<LessonDtoOut>();
        var dblessons = await _lessonService.GetLessonsFromUnityId(unity.Id);

        foreach (var lesson in dblessons)
        {
            lessons.Add(new LessonDtoOut()
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                VideoUrl = lesson.VideoUrl,
                Concluded = await _questionnaireUseCase.QuestionsAreAlreadyAnswered(userId, lesson.Id),
            });
        }

        return lessons;
    }

    public async Task<LessonDtoOut?> GetLesson(string unityName, int lessonId, string userId)
    {
        var unity = await _unityService.GetUnityByName(unityName);
        if (unity is null) return null;

        var lessonFromDb = await _lessonService.GetLessonById(lessonId);
        if (lessonFromDb is null) return null;

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

        return new LessonDtoOut
        {
            Id = lessonFromDb.Id,
            Description = lessonFromDb.Description,
            Title = lessonFromDb.Title,
            VideoUrl = lessonFromDb.VideoUrl,
            Questions = questions,
            Concluded = await _questionnaireUseCase.QuestionsAreAlreadyAnswered(userId, lessonFromDb.Id),
        };
    }
}
