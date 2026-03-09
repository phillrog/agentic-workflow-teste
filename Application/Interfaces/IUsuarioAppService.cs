using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces;

public interface IUsuarioAppService
{
    Task<Result<UsuarioResponse>> CreateAsync(UsuarioCreateRequest request);
    Task<Result<UsuarioResponse>> UpdateAsync(int id, UsuarioUpdateRequest request);
    Task<Result<IEnumerable<UsuarioResponse>>> GetAllAsync();
    Task<Result<UsuarioResponse>> GetByIdAsync(int id);
    Task<Result> DeleteAsync(int id);
}