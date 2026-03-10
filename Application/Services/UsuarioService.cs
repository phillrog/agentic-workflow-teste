using CleanArch.Application.DTOs;
using CleanArch.Domain.Common;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;

namespace CleanArch.Application.Services;

public class UsuarioService(IUsuarioRepository repository)
{
    public async Task<Result<Guid>> CreateAsync(UsuarioRequest request)
    {
        var usuarioResult = Usuario.Create(request.Nome, request.Email);
        if (usuarioResult.IsFailure) return Result.Failure<Guid>(usuarioResult.Error);

        await repository.AddAsync(usuarioResult.Value);
        return Result.Success(usuarioResult.Value.Id);
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAllAsync()
    {
        var usuarios = await repository.GetAllAsync();
        return usuarios.Select(u => new UsuarioResponse(u.Id, u.Nome, u.Email.Address, u.Status.ToString()));
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user == null) return Result.Failure("Usuário não encontrado.");
        
        await repository.DeleteAsync(id);
        return Result.Success();
    }
}