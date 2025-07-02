using Domain.Models;

namespace Domain.Interfaces;

public interface ILessonService
{
    public Task<List<Lesson>> GetLessonsFromUnityId(int unityId);
    public Task<Lesson?> GetLessonByIdAndUnity(int unityId, int lessonId);
}
