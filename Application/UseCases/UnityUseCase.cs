using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;

namespace Application.UseCases
{
    public class UnityUseCase(IUnityService _unityService)
    {
        public async Task<List<UnityDtoOut>> GetUnities()
        {

            var unities = new List<UnityDtoOut>();
            var dbUnities = await _unityService.GetUnities();

        foreach (var unity in dbUnities)
        {
            unities.Add(new UnityDtoOut()
            {
                Id = unity.Id,
                Name = unity.Name,
                Description = unity.Description
            });
        }

        return unities;
        }
    }
}
