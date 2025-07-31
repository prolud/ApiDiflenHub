using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class CertificateService(IUnityRepository unityRepository, ICertificateRepository certificateRepository) : ICertificateService
    {
        public async Task<bool> WasCertificateAlreadyIssued(string userId, string unityName)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return false;

            var certificate = await certificateRepository.GetAsync(c => c.UserId == int.Parse(userId) && c.UnityId == unity.Id);
            return certificate is not null;
        }
    }
}