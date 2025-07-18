using System.ComponentModel;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class LessonService(AppDbContext _context) : ILessonService
    {
        public async Task<Lesson?> GetLessonByIdAndUnity(int unityId, int lessonId)
        {
            return await _context.Lessons
                .Where(_ => _.UnityId == unityId && _.Id == lessonId)
                    .Include(_ => _.Questions)
                        .ThenInclude(q => q.Alternatives)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Lesson>> GetLessonsFromUnityId(int unityId)
        {
            return await _context.Lessons
                .Where(_ => _.UnityId == unityId)
                .OrderBy(l => l.Sequence)
                .ToListAsync();
        }
    }
}
