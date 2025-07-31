using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IUnityService
    {
        Task<bool> WasAllQuestionsCorrectlyAnswered(string unityName, string userId);
    }
}
