using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class CertificateService(AppDbContext _context) : ICertificateService
    {
        public async Task<Certificate?> GetAsync(int userId, int unityId)
        {
            return await _context.Certificates
            .FirstOrDefaultAsync(c => c.UserId == userId && c.UnityId == unityId);
        }

        public async Task InsertAsync(Certificate user)
        {
            await _context.Certificates.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}