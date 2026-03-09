namespace Application.DTOs;

public record UsuarioCreateRequest(string Nome, string Email);
public record UsuarioUpdateRequest(string Nome, string Email, bool Status);
public record UsuarioResponse(int Id, string Nome, string Email, bool Status);