using CleanArch.Application.DTOs;
using CleanArch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(UsuarioService usuarioService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(UsuarioRequest request)
    {
        var result = await usuarioService.CreateAsync(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await usuarioService.GetAllAsync());

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await usuarioService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }
}