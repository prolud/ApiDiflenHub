using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class UnityService(AppDbContext _context) : IUnityService
    {
        public async Task<List<Unity>> GetUnities()
        {
            return await _context.Unities
                .ToListAsync();
        }
    }
}
