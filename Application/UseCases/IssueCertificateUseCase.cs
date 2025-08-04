using System.Net;
using Application.UseCases.Common;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Application.UseCases
{
    public class IssueCertificateUseCase(
        ICertificateService certificateService,
        IQuestionService questionService,
        ICertificateRepository certificateRepository,
        IUnityRepository unityRepository)
    {
        public async Task<UseCaseResult> ExecuteAsync(string userId, string unityName)
        {
            var result = new UseCaseResult();

            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new()
            {
                Content = "Unidade inválida",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (await certificateService.WasCertificateAlreadyIssued(userId, unity.Id))
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = "O Certificado já havia sido emitido."
                };
            }

            if (await questionService.WasAllQuestionsCorrectlyAnswered(unity.Id, userId))
            {
                var certificate = new Certificate
                {
                    CreatedAt = DateTime.Now,
                    UnityId = unity.Id,
                    UserId = int.Parse(userId)
                };

                await certificateRepository.InsertAsync(certificate);
                
                result = new ()
                {
                    Content = "Certificado emitido com sucesso!",
                };
            }
            else
            {
                result = new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = "Nem todas as questões foram respondidas corretamente.",
                };
            }

            return result;
        }
    }
}