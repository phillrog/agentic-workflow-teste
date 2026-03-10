using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(Guid id);
}