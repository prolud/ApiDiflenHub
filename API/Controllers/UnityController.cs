using Application.UseCases;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/unity")]
[ApiController]
[Authorize]
public class UnityController(IUnityRepository unityRepository) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllUnities()
    {
        var unities = await unityRepository.GetListAsync(u => u.Id > 0);
        return Ok(unities.Select(unity => new UnityDtoOut
        {
            Id = unity.Id,
            Name = unity.Name,
            Description = unity.Description,
        }));
    }

    [HttpGet("get-from-name")]
    public async Task<IActionResult> GetUnity([FromQuery] string unityName)
    {
        var unity = await unityRepository.GetAsync(u => u.Name == unityName);

        if (unity is null) return NoContent();

        return Ok(unity);
    }
}
