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
        const int DEFAULT_POINTS = 300;

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

        public async Task<GetLastAnswersOut> GetLastAnswersAsync(string userId, int lessonId)
        {
            var userAnswers = await _answerService.GetAnswersByUserAndLesson(int.Parse(userId), lessonId);

            var answers = GetAnswerVerifyOuts(userAnswers);

            return new GetLastAnswersOut
            {
                Answers = answers,
                CurrentPointsWeight = CalculatePoints(userAnswers)
            };
        }

        private static int CalculatePoints(List<Answer> userAnswers)
        {
            int qtdErros = userAnswers.Where(ua => !ua.IsCorrect).Count();
            int pontosPerdidos = qtdErros * 50;
            int points = DEFAULT_POINTS - pontosPerdidos;

            if (points <= 0)
            {
                points = 50;
            }

            return points;
        }

        private static List<AnswerVerifyOut> GetAnswerVerifyOuts(List<Answer> userAnswers)
        {
            var lessonQuestionsIds = userAnswers
            .GroupBy(a => a.QuestionId)
            .Select(g => g.Key)
            .ToList();

            var lastAnswers = lessonQuestionsIds
            .Select(lqi => userAnswers.First(a => a.QuestionId == lqi))
            .ToList();

            var answers = lastAnswers
            .Select(la => new AnswerVerifyOut
            {
                AlternativeId = la.AlternativeId,
                QuestionId = la.QuestionId,
                IsCorrect = la.IsCorrect,
            })
            .ToList();

            return answers;
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

        public async Task<bool> QuestionsAreAlreadyAnswered(string userId, int lessonId)
        {
            var oldLastAnswers = await GetLastAnswersAsync(userId, lessonId);

            if (oldLastAnswers.Answers.Count == 0)
            {
                return false;
            }
            var theresAnswers = oldLastAnswers.Answers.Count > 0;
            var theresWrongAnswers = oldLastAnswers.Answers.Any(la => !la.IsCorrect);
            var allAnswersAreCorrect = !theresWrongAnswers;

            return theresAnswers && allAnswersAreCorrect;
        }
    }
}