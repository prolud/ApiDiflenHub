using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/unity")]
[ApiController]
[Authorize]
public class UnityController(UnityUseCase _unityUseCase) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllUnities()
    {
        return Ok(await _unityUseCase.GetUnities());
    }

    [HttpGet("get-from-name")]
    public async Task<IActionResult> GetUnity([FromQuery] string unityName)
    {
        var unity = await _unityUseCase.GetUnityFromName(unityName);

        if (unity is null) return NoContent();

        return Ok(unity);
    }
}
