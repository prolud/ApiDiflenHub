using Domain.Models;

namespace Domain.Interfaces
{
    public interface IQuestionService
    {
        public Task<Question> GetQuestion(int id);
    }
}