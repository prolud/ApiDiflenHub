using Domain.Interfaces;
using Domain.Models;

namespace Infra.Services
{
    public class AnswerService(AppDbContext _context) : IAnswerService
    {
        public async Task InsertAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }
    }
}