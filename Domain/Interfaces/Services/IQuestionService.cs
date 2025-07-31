using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        public Task<Question> GetQuestionAsync(int id);
        public Task<List<Question>> GetQuestionsByLessonIdAsync(int lessonId);
        public Task<List<Question>> GetQuestionsFromUnity(int unityId);
    }
}