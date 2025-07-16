using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class QuestionnaireUseCase(
        IAlternativeService _alternativeService,
        IAnswerService _answerService,
        IQuestionService _questionService)
    {
        public async Task<int?> VerifyAnswersAsync(List<AnswerVerifyIn> answersVerifyIn, string userId)
        {
            int? lessonId = null;
            foreach (var answerVerifyIn in answersVerifyIn)
            {
                var correctAlternative = await _alternativeService.GetCorrectAlternativeAsync(answerVerifyIn.QuestionId);
                if (correctAlternative is null) return null;

                var question = await _questionService.GetQuestionAsync(correctAlternative.QuestionId);
                lessonId = question.LessonId;

                await _answerService.InsertAnswerAsync(new Answer
                {
                    AlternativeId = answerVerifyIn.AlternativeId,
                    UserId = int.Parse(userId),
                    QuestionId = question.Id,
                    LessonId = question.LessonId,
                    IsCorrect = answerVerifyIn.AlternativeId == correctAlternative.Id,
                    Created = DateTime.Now,
                });
            }

            return lessonId;
        }

        public async Task<List<AnswerVerifyOut>> GetLastAnswersAsync(int lessonId, string userId)
        {
            var questionsFromLesson = await _questionService.GetQuestionsByLessonIdAsync(lessonId);
            
            var lastAnswers = await _answerService.GetLastAnswersAsync(lessonId, int.Parse(userId));
            return lastAnswers
            .Select(la => new AnswerVerifyOut
            {
                AlternativeId = la.AlternativeId,
                QuestionId = la.QuestionId,
                IsCorrect = la.IsCorrect,
            })
            .ToList();
        }
    }
}