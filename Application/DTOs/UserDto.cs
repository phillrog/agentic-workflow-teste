namespace Application.DTOs;

public record UserRequest(string Name, string Email);
public record UserResponse(Guid Id, string Name, string Email);