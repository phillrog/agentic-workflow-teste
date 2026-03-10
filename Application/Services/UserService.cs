using Application.DTOs;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserResponse>> CreateAsync(UserRequest request)
    {
        var user = User.Create(request.Name, request.Email);
        await _repository.AddAsync(user);
        return Result.Success(new UserResponse(user.Id, user.Name, user.Email));
    }

    public async Task<Result<UserResponse>> GetByIdAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return Result.Failure<UserResponse>("User not found");
        
        return Result.Success(new UserResponse(user.Id, user.Name, user.Email));
    }

    public async Task<Result<IEnumerable<UserResponse>>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        var response = users.Select(u => new UserResponse(u.Id, u.Name, u.Email));
        return Result.Success(response);
    }

    public async Task<Result> UpdateAsync(Guid id, UserRequest request)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return Result.Failure("User not found");

        user.Update(request.Name, request.Email);
        await _repository.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
        return Result.Success();
    }
}