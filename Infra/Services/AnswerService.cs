using Domain.Interfaces.Services;
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

        public async Task<List<Answer>> GetAnswersByUserAndUnity(int unityId, int userId)
        {
            return await _context.Answers
                .Include(a => a.Question)
                .Where(a => a.UserId == userId && a.UnityId == unityId)
                .OrderByDescending(a => a.Created)
                .ToListAsync();
        }

        public async Task InsertAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAnswersAsync(List<Answer> answers)
        {
            _context.Answers.AddRange(answers);
            await _context.SaveChangesAsync();
        }
    }
}