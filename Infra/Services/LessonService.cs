using System.ComponentModel;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class LessonService(AppDbContext _context) : ILessonService
    {
        public async Task<bool> LessonAreAlreadyAnswered(string userId, int lessonId)
        {
            var oldLastAnswers = await GetLastAnswersAsync(userId, lessonId);

            if (oldLastAnswers.Answers.Count == 0)
            {
                return false;
            }
            var theresAnswers = oldLastAnswers.Answers.Count > 0;
            var theresWrongAnswers = oldLastAnswers.Answers.Any(la => !la.IsCorrect);
            var allAnswersAreCorrect = !theresWrongAnswers;

            return theresAnswers && allAnswersAreCorrect;
        }
    }
}
