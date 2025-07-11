using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class QuestionnaireUseCase(IAlternativeService _alternativeService, IAnswerService _answerService)
    {
        public async Task<List<AnswerVerifyOut>?> VerifyAnswersAsync(List<AnswerVerifyIn> answersVerifyIn)
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
                    QuestionId = answerVerifyIn.QuestionId,
                    UserId = answerVerifyIn.UserId,
                    Created = DateTime.Now,
                });
            }

            return answersVerifyOut;
        }
    }
}