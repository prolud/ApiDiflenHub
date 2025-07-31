using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infra.Services
{
    public class UnityService(IUnityRepository unityRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository) : IUnityService
    {
        public async Task<bool> WasAllQuestionsCorrectlyAnswered(string unityName, string userId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return false;

            var questionsFromUnity = await questionRepository.GetListAsync(q => q.UnityId == unity.Id);
            var questionIds = questionsFromUnity.Select(q => q.Id).ToList();

            var unityAnswersFromUser = await answerRepository.GetListAsync(a => a.UnityId == unity.Id && a.UserId == int.Parse(userId));

            foreach (var questionId in questionIds)
            {
                if (!unityAnswersFromUser.Any(a => a.QuestionId == questionId && a.IsCorrect))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
