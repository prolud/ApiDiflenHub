using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infra.Services
{
    public class CertificateService(ICertificateRepository certificateRepository) : ICertificateService
    {
        public async Task<bool> WasCertificateAlreadyIssued(string userId, int unityId)
        {
            var certificate = await certificateRepository.GetAsync(c => c.UserId == int.Parse(userId) && c.UnityId == unityId);
            return certificate is not null;
        }
    }
}