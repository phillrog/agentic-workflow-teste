using Application.DTOs;
using Domain.Common;

namespace Application.Services;

public interface IUserService
{
    Task<Result<UserResponse>> CreateAsync(UserRequest request);
    Task<Result<UserResponse>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<UserResponse>>> GetAllAsync();
    Task<Result> UpdateAsync(Guid id, UserRequest request);
    Task<Result> DeleteAsync(Guid id);
}