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
        public async Task<List<AnswerVerifyOut>?> VerifyAnswersAsync(List<AnswerVerifyIn> answersVerifyIn, string userId)
        {
            List<AnswerVerifyOut> answersVerifyOut = [];

            foreach (var answerVerifyIn in answersVerifyIn)
            {
                var correctAlternative = await _alternativeService.GetCorrectAlternativeAsync(answerVerifyIn.QuestionId);
                if (correctAlternative is null) return null;

                var question = await _questionService.GetQuestion(correctAlternative.QuestionId);

                answersVerifyOut.Add(new AnswerVerifyOut
                {
                    QuestionId = answerVerifyIn.QuestionId,
                    AlternativeId = answerVerifyIn.AlternativeId,
                    IsCorrect = answerVerifyIn.AlternativeId == correctAlternative.Id
                });

                await _answerService.UpsertAnswerAsync(new Answer
                {
                    AlternativeId = answerVerifyIn.AlternativeId,
                    UserId = int.Parse(userId),
                    QuestionId = question.Id,
                    LessonId = question.LessonId,
                    IsCorrect = answerVerifyIn.AlternativeId == correctAlternative.Id,
                    Created = DateTime.Now,
                });
            }

            return answersVerifyOut;
        }

        public async Task<List<AnswerVerifyOut>> GetLastAnsersAsync(int lessonId, string userId)
        {
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