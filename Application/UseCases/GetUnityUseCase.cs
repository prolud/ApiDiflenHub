using System.Net;
using Application.UseCases.Common;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetUnityUseCase(IUnityRepository unityRepository, ICertificateRepository certificateRepository, IQuestionService questionService)
    {
        public async Task<UseCaseResult> ExecuteAsync(string unityName, string userId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new()
            {
                StatusCode = HttpStatusCode.NoContent
            };

            var certificate = await certificateRepository.GetAsync(c => c.UserId == int.Parse(userId) && c.UnityId == unity.Id);

            return new()
            {
                Content = new UnityDtoOut
                {
                    Id = unity.Id,
                    Description = unity.Description,
                    Name = unity.Name,
                    WasCertificateAlreadyIssued = certificate is not null,
                    WasAllQuestionsCorrectlyAnswered = await questionService.WasAllQuestionsCorrectlyAnswered(unity.Id, userId)
                }
            };
        }
    }
}