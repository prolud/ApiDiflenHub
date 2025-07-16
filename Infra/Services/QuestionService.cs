using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class QuestionService(AppDbContext _context) : IQuestionService
    {
        public async Task<Question> GetQuestionAsync(int id)
        {
            return await _context.Questions.FirstAsync(q => q.Id == id);
        }

        public async Task<List<Question>> GetQuestionsByLessonIdAsync(int lessonId)
        {
            return await _context.Questions
                .Where(q => q.LessonId == lessonId)
                .ToListAsync();
        }
    }
}