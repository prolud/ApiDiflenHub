using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ICertificateRepository : IBaseRepository<Certificate>
    {
        Task<List<Certificate>> GetCertificatesByUserId(int userId);
    }
}