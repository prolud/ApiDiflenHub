using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AnswerService(AppDbContext _context) : IAnswerService
    {
        public async Task<List<Answer>> GetAnswersByUserAndLesson(int userId, int lessonId)
        {
            return await _context.Answers
                .Where(a => a.UserId == userId && a.LessonId == lessonId)
                .OrderByDescending(a => a.Created)
                .ToListAsync();
        }

        public async Task InsertAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
    }
}