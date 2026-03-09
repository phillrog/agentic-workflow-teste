using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioAppService _appService;

    public UsuarioController(IUsuarioAppService appService)
    {
        _appService = appService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appService.GetAllAsync();
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _appService.GetByIdAsync(id);
        if (result.IsFailure) return NotFound(result.Error);
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsuarioCreateRequest request)
    {
        var result = await _appService.CreateAsync(request);
        if (result.IsFailure) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UsuarioUpdateRequest request)
    {
        var result = await _appService.UpdateAsync(id, request);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _appService.DeleteAsync(id);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }
}