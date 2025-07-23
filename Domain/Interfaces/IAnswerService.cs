using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAnswerService
    {
        public Task InsertAnswerAsync(Answer answer);
        public Task<List<Answer>> GetAnswersByUserAndLesson(int lessonId, int userId);
        public Task InsertAnswersAsync(List<Answer> answers);
    }
}