using System.Runtime.InteropServices;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases;

public class LessonUseCase(ILessonService _lessonService, IUnityService _unityService)
{
    public async Task<List<LessonDtoOut>> GetLessonsByUnityId(int unityId)
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
                VideoUrl = lesson.VideoUrl
            });
        }

        return lessons;
    }

    public async Task<List<LessonDtoOut>> GetLessons(string unityName)
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
                VideoUrl = lesson.VideoUrl
            });
        }

        return lessons;
    }

    public async Task<Lesson?> GetLesson(string unityName, int lessonId)
    {
        var unity = await _unityService.GetUnityByName(unityName);
        if (unity is null) return null;

        return await _lessonService.GetLessonByIdAndUnity(unity.Id, lessonId);
    }
}
