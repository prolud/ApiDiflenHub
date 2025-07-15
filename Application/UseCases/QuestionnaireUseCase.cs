using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class QuestionnaireUseCase(IAlternativeService _alternativeService, IAnswerService _answerService)
    {
        public async Task<List<AnswerVerifyOut>?> VerifyAnswersAsync(List<AnswerVerifyIn> answersVerifyIn, string userId)
        {
            List<AnswerVerifyOut> answersVerifyOut = [];

            foreach (var answerVerifyIn in answersVerifyIn)
            {
                var correctAlternativeId = await _alternativeService.GetCorrectAlternativeIdAsync(answerVerifyIn.QuestionId);

                if (correctAlternativeId is null) return null;

                answersVerifyOut.Add(new AnswerVerifyOut
                {
                    QuestionId = answerVerifyIn.QuestionId,
                    AlternativeId = answerVerifyIn.AlternativeId,
                    IsCorrect = answerVerifyIn.AlternativeId == correctAlternativeId
                });

                await _answerService.InsertAnswerAsync(new Answer
                {
                    AlternativeId = answerVerifyIn.AlternativeId,
                    UserId = int.Parse(userId),
                    Created = DateTime.Now,
                });
            }

            return answersVerifyOut;
        }
    }
}