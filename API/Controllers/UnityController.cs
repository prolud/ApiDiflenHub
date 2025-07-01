using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/unity")]
public class UnityController(UnityUseCase _unityUseCase) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllUnities()
    {
        return Ok(await _unityUseCase.GetUnities());
    }
}
