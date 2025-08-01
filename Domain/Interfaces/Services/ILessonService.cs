using Domain.Models;

namespace Domain.Interfaces.Services;

public interface ILessonService
{
    Task<bool> LessonAreAlreadyAnswered(string userId, int lessonId);
}
