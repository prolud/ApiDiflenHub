using Domain.DTOs;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IAnswerService
    {
        Task<GetLastAnswersOut> GetLastAnswersAsync(string userId, int lessonId);
        Task<GetLastAnswersOut?> VerifyAnswersAsync(AnswerVerifyIn answerVerifyIn, string userId, int unityId);
    }
}