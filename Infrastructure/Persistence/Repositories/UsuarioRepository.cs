using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<Usuario> _validator;

    public UsuarioRepository(ApplicationDbContext context, IValidator<Usuario> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<Usuario>> GetByIdAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        return usuario == null 
            ? Result<Usuario> .Failure("Usuário não encontrado.") 
            : Result<Usuario>.Success(usuario);
    }

    public async Task<Result<IEnumerable<Usuario>>> GetAllAsync()
    {
        var lista = await _context.Usuarios.ToListAsync();
        return Result<IEnumerable<Usuario>>.Success(lista);
    }

    public async Task<Result<Usuario>> AddAsync(Usuario usuario)
    {
        var validationResult = await _validator.ValidateAsync(usuario);
        if (!validationResult.IsValid)
            return Result<Usuario>.Failure(validationResult.Errors.First().ErrorMessage);

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return Result<Usuario>.Success(usuario);
    }

    public async Task<Result> UpdateAsync(Usuario usuario)
    {
        var validationResult = await _validator.ValidateAsync(usuario);
        if (!validationResult.IsValid)
            return Result.Failure(validationResult.Errors.First().ErrorMessage);

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return Result.Failure("Usuário inexistente.");

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}