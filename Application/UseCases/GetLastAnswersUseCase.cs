using System.Net;
using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLastAnswersUseCase(IAnswerService answerService, IQuestionService questionService, ICertificateService certificateService, IUnityRepository unityRepository)
    {
        public async Task<UseCaseResult> ExecuteAsync(string userId, int lessonId, string unityName)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = "Nenhuma unidade foi encontrada."
            };

            var result = await answerService.GetLastAnswersAsync(userId, lessonId);

            result.WasAllQuestionsCorrectlyAnswered = await questionService.WasAllQuestionsCorrectlyAnswered(unity.Id, userId);
            result.WasCertificateAlreadyIssued = await certificateService.WasCertificateAlreadyIssued(userId, unity.Id);
            
            return new()
            {
                Content = result
            };
        }
    }
}