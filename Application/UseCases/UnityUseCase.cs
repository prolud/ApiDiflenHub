using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class UnityUseCase(IUnityService _unityService)
    {
        public async Task<List<Unity>> GetUnities() => await _unityService.GetUnities();
    }
}
