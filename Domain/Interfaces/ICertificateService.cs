using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICertificateService
    {
        public Task InsertAsync(Certificate user);
        public Task<Certificate?> GetAsync(int userId, int unityId);
    }
}