using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AlternativeService(AppDbContext _context) : IAlternativeService
    {
        public async Task<Alternative?> GetCorrectAlternativeAsync(int questionId)
        {
            var correctAlternative = await _context.Alternatives
            .Where(a => a.QuestionId == questionId && a.IsCorrect)
            .Include(a => a.Question)
            .FirstOrDefaultAsync();

            return correctAlternative;
        }
    }
}