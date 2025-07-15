using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AnswerService(AppDbContext _context) : IAnswerService
    {
        public async Task<List<Answer>> GetLastAnswersAsync(int lessonId, int userId)
        {
            return await _context.Answers
                .Where(a => a.UserId == userId && a.LessonId == lessonId)
                .ToListAsync();
        }

        public async Task UpsertAnswerAsync(Answer answer)
        {
            var existingAnswer = await _context.Answers
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.UserId == answer.UserId && a.QuestionId == answer.QuestionId);

            if (existingAnswer is not null) answer.Id = existingAnswer.Id;

            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
        }
    }
}