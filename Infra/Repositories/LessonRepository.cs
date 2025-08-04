using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class LessonRepository(AppDbContext context) : BaseRepository<Lesson>(context), ILessonRepository
    {
        public async Task<Lesson?> GetLesson(int lessonId)
        {
            return await context.Lessons
            .Include(l => l.Questions)
            .ThenInclude(q => q.Alternatives)
            .FirstOrDefaultAsync(l => l.Id == lessonId);
        }
    }
}