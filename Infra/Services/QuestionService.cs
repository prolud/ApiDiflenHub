using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infra.Services
{
    public class QuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository) : IQuestionService
    {
        public async Task<bool> WasAllQuestionsCorrectlyAnswered(int unityId, string userId)
        {
            var questionsFromUnity = await questionRepository.GetListAsync(q => q.UnityId == unityId);
            var questionIds = questionsFromUnity.Select(q => q.Id).ToList();

            var unityAnswersFromUser = await answerRepository.GetListAsync(a => a.UnityId == unityId && a.UserId == int.Parse(userId));

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