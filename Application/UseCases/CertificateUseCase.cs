using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class CertificateUseCase(
        ICertificateService _service,
        IUnityService _unityService,
        IAnswerService _answerService,
        IQuestionService _questionService)
    {
        public async Task<bool?> IssueNewCertificate(string userId, string unityName)
        {
            var unity = await _unityService.GetUnityByName(unityName);
            if (unity is null) return null;

            if (await WasCertificateAlreadyIssued(userId, unityName))
            {
                return false;
            }

            if (await WasAllQuestionsCorrectlyAnswered(unityName, userId))
            {
                await _service.InsertAsync(new Certificate
                {
                    CreatedAt = DateTime.Now,
                    UnityId = unity.Id,
                    UserId = int.Parse(userId)
                });
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<bool> WasAllQuestionsCorrectlyAnswered(string unityName, string userId)
        {
            var unity = await _unityService.GetUnityByName(unityName);

            if (unity is null) return false;

            var questionsFromUnity = await _questionService.GetQuestionsFromUnity(unity.Id);
            var questionIds = questionsFromUnity.Select(q => q.Id).ToList();

            var unityAnswers = await _answerService.GetAnswersByUserAndUnity(unity.Id, int.Parse(userId));

            foreach (var questionId in questionIds)
            {
                if (!unityAnswers.Any(a => a.QuestionId == questionId && a.IsCorrect))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> WasCertificateAlreadyIssued(string userId, string unityName)
        {
            var unity = await _unityService.GetUnityByName(unityName);
            if (unity is null) return false;

            var certificate = await _service.GetAsync(int.Parse(userId), unity.Id);
            return certificate is not null;
        }
    }
}