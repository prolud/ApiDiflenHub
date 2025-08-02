using System.Net;
using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class VerifyAnswersUseCase(IUnityRepository unityRepository, ILessonService lessonService, IAnswerService answerService, IQuestionService questionService, ICertificateService certificateService)
    {
        public async Task<UseCaseResult<GetLastAnswersOut>> ExecuteAsync(AnswerVerifyIn answerVerifyIn, string userId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == answerVerifyIn.UnityName);
            if (unity is null)
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "O nome da unidade está incorreto."
                };
            }

            var lessonId = answerVerifyIn.LessonId;
            if (await lessonService.LessonAreAlreadyAnswered(userId, lessonId))
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Todas as questões já foram respondidas",
                };
            }

            var verifiedAnswers = await answerService.VerifyAnswersAsync(answerVerifyIn, userId, unity.Id);
            if (verifiedAnswers is null)
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Não foi possível encontrar uma das alternativas de alguma questão",
                };
            }

            verifiedAnswers.WasAllQuestionsCorrectlyAnswered = await questionService.WasAllQuestionsCorrectlyAnswered(unity.Id, userId);
            verifiedAnswers.WasCertificateAlreadyIssued = await certificateService.WasCertificateAlreadyIssued(userId, unity.Id);

            return new()
            {
                Content = verifiedAnswers,
            };
        }
    }
}