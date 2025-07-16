using Domain.Models;

namespace Domain.Interfaces
{
    public interface IQuestionService
    {
        public Task<Question> GetQuestionAsync(int id);
        public Task<List<Question>> GetQuestionsByLessonIdAsync(int lessonId);
    }
}