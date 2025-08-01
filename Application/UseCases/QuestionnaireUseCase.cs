using Domain.DTOs;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Application.UseCases
{
    public class QuestionnaireUseCase(
        IAlternativeService _alternativeService,
        IAnswerService _answerService,
        IUserService _userService,
        IUnityService _unityService)
    {
        public async Task<GetLastAnswersOut?> VerifyAnswersAsync(List<AnswerVerifyIn> answersVerifyIn, string userId)
        {
            var answersToInsert = new List<Answer>();
            var answerWithUnityName = answersVerifyIn.FirstOrDefault(a => !string.IsNullOrEmpty(a.UnityName));

            if (answerWithUnityName is not null)
            {
                var unityName = answerWithUnityName.UnityName;

                foreach (var answerVerifyIn in answersVerifyIn)
                {
                    var correctAlternative = await _alternativeService.GetCorrectAlternativeAsync(answerVerifyIn.QuestionId);
                    if (correctAlternative is null) return null;

                    var unity = await _unityService.GetUnityByName(unityName);

                    answersToInsert.Add(new Answer
                    {
                        AlternativeId = answerVerifyIn.AlternativeId,
                        UserId = int.Parse(userId),
                        QuestionId = correctAlternative.Question.Id,
                        LessonId = correctAlternative.Question.LessonId,
                        UnityId = unity!.Id,
                        IsCorrect = answerVerifyIn.AlternativeId == correctAlternative.Id,
                        Created = DateTime.Now,
                    });
                }
            }
            await _answerService.InsertAnswersAsync(answersToInsert);

            var lastAnswers = await GetLastAnswersAsync(userId, answersVerifyIn.First().LessonId);
            if (!lastAnswers.Answers.Any(ai => !ai.IsCorrect))
            {
                await _userService.AddExperience(lastAnswers.CurrentPointsWeight, int.Parse(userId));
            }

            return lastAnswers;
        }

        public bool TheresMoreThanOneLessonId(List<AnswerVerifyIn> answersVerifyIn)
        {
            var qtdLessonIds = answersVerifyIn
                .GroupBy(av => av.LessonId)
                .Select(g => g.Key)
                .ToList()
                .Count;

            return qtdLessonIds > 1;
        }
    }
}