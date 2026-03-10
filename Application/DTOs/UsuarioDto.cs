namespace CleanArch.Application.DTOs;

public record UsuarioRequest(string Nome, string Email);
public record UsuarioResponse(Guid Id, string Nome, string Email, string Status);