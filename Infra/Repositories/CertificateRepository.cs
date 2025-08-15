using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class CertificateRepository(AppDbContext context) : BaseRepository<Certificate>(context), ICertificateRepository
    {
        public async Task<List<Certificate>> GetCertificatesByUserId(int userId)
        {
            return await context.Certificates
                .Where(c => c.UserId == userId)
                .Include(c => c.Unity)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}