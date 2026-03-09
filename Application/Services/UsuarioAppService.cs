using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioAppService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<UsuarioResponse>> CreateAsync(UsuarioCreateRequest request)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure) return Result.Failure<UsuarioResponse>(emailResult.Error);

        var usuario = new Usuario(request.Nome, emailResult.Value);
        
        await _repository.AddAsync(usuario);
        await _repository.SaveChangesAsync();

        return Result.Success(new UsuarioResponse(usuario.Id, usuario.Nome, usuario.Email.Address, usuario.Status));
    }

    public async Task<Result<IEnumerable<UsuarioResponse>>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        var response = usuarios.Select(u => new UsuarioResponse(u.Id, u.Nome, u.Email.Address, u.Status));
        return Result.Success(response);
    }

    public async Task<Result<UsuarioResponse>> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return Result.Failure<UsuarioResponse>("Usuário não encontrado.");

        return Result.Success(new UsuarioResponse(usuario.Id, usuario.Nome, usuario.Email.Address, usuario.Status));
    }

    public async Task<Result<UsuarioResponse>> UpdateAsync(int id, UsuarioUpdateRequest request)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return Result.Failure<UsuarioResponse>("Usuário não encontrado.");

        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure) return Result.Failure<UsuarioResponse>(emailResult.Error);

        usuario.Update(request.Nome, emailResult.Value);
        usuario.AlterarStatus(request.Status);

        _repository.Update(usuario);
        await _repository.SaveChangesAsync();

        return Result.Success(new UsuarioResponse(usuario.Id, usuario.Nome, usuario.Email.Address, usuario.Status));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return Result.Failure("Usuário não encontrado.");

        _repository.Delete(usuario);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }
}