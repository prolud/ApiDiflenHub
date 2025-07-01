using Domain.Models;

namespace Domain.Interfaces;

public interface ILessonService
{
    public Task<Lesson> GetLessonsFromUnity();
}
