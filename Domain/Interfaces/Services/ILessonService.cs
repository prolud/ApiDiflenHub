using Domain.Models;

namespace Domain.Interfaces.Services;

public interface ILessonService
{
    public Task<List<Lesson>> GetLessonsFromUnityId(int unityId);
    public Task<Lesson?> GetLessonById(int lessonId);
}
