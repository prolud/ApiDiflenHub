using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class QuestionService(AppDbContext _context) : IQuestionService
    {
        public async Task<Question> GetQuestion(int id)
        {
            return await _context.Questions.FirstAsync(q => q.Id == id);
        }
    }
}