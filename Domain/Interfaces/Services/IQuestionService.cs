using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<bool> WasAllQuestionsCorrectlyAnswered(int unityId, string userId);
    }
}