using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Result<Usuario>> GetByIdAsync(int id);
    Task<Result<IEnumerable<Usuario>>> GetAllAsync();
    Task<Result<Usuario>> AddAsync(Usuario usuario);
    Task<Result> UpdateAsync(Usuario usuario);
    Task<Result> DeleteAsync(int id);
}