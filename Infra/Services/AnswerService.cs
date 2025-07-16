using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AnswerService(AppDbContext _context) : IAnswerService
    {
        public async Task<List<Answer>> GetLastAnswersAsync(int lessonId, int userId)
        {
            var userAnswers = await _context.Answers
                .Where(a => a.UserId == userId && a.LessonId == lessonId)
                .OrderByDescending(a => a.Created)
                .ToListAsync();

            var lessonQuestionsIds = userAnswers
                .GroupBy(a => a.QuestionId)
                .Select(g => g.Key)
                .ToList();

            var latestResposes = new List<Answer>();

            foreach (var lessonQuestionsId in lessonQuestionsIds)
            {
                latestResposes.Add(userAnswers.First(a => a.QuestionId == lessonQuestionsId));
            }

            return latestResposes;
        }

        public async Task InsertAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
    }
}