using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUnityService
    {
        public Task<List<Unity>> GetUnities();
    }
}
