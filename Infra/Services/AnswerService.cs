using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AnswerService(AppDbContext _context, IAlternativeService _alternativeService, IAnswerRepository unityRepository, IUserService _userService) : IAnswerService
    {
        const int DEFAULT_POINTS = 300;

        private async Task<List<Answer>> GetAnswersByUserAndLesson(int userId, int lessonId)
        {
            return await _context.Answers
                .Where(a => a.UserId == userId && a.LessonId == lessonId)
                .OrderByDescending(a => a.Created)
                .ToListAsync();
        }

        public async Task<GetLastAnswersOut> GetLastAnswersAsync(string userId, int lessonId)
        {
            var userAnswers = await GetAnswersByUserAndLesson(int.Parse(userId), lessonId);

            var answers = GetAnswerVerifyOuts(userAnswers);

            return new GetLastAnswersOut
            {
                Answers = answers,
                CurrentPointsWeight = CalculatePoints(userAnswers)
            };
        }

        public async Task<GetLastAnswersOut?> VerifyAnswersAsync(AnswerVerifyIn answerVerifyIn, string userId, int unityId)
        {
            var answersToInsert = new List<Answer>();

            foreach (var answer in answerVerifyIn.Answers)
            {
                var correctAlternative = await _alternativeService.GetCorrectAlternativeAsync(answer.QuestionId);
                if (correctAlternative is null) return null;

                answersToInsert.Add(new Answer
                {
                    AlternativeId = answer.AlternativeId,
                    UserId = int.Parse(userId),
                    QuestionId = answer.QuestionId,
                    LessonId = answerVerifyIn.LessonId,
                    UnityId = unityId,
                    IsCorrect = answer.AlternativeId == correctAlternative.Id,
                    Created = DateTime.Now,
                });
            }

            await unityRepository.InsertRangeAsync(answersToInsert);

            var lastAnswers = await GetLastAnswersAsync(userId, answerVerifyIn.LessonId);

            if (!lastAnswers.Answers.Any(ai => !ai.IsCorrect))
            {
                await _userService.AddExperience(lastAnswers.CurrentPointsWeight, int.Parse(userId));
            }

            return lastAnswers;
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

        private static int CalculatePoints(List<Answer> userAnswers)
        {
            int qtdErros = userAnswers.Count(ua => !ua.IsCorrect);
            int pontosPerdidos = qtdErros * 50;
            int points = DEFAULT_POINTS - pontosPerdidos;

            if (points <= 0)
            {
                points = 50;
            }

            return points;
        }
    }
}