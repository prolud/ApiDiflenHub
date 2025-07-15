using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AlternativeService(AppDbContext _context) : IAlternativeService
    {
        public async Task<Alternative?> GetCorrectAlternativeAsync(int questionId)
        {
            var correctAlternative = await _context.Alternatives.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.IsCorrect);

            if (correctAlternative is null) return null;

            return correctAlternative;
        }
    }
}