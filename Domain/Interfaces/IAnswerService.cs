using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAnswerService
    {
        public Task UpsertAnswerAsync(Answer answer);
        public Task<List<Answer>> GetLastAnswersAsync(int lessonId, int userId);
    }
}