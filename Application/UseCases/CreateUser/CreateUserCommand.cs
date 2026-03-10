using MediatR;

namespace Application.UseCases.CreateUser;

/// <summary>
/// Utilizando 'record' para imutabilidade e simplificação de comandos.
/// </summary>
public record CreateUserCommand(string Name, string Email) : IRequest<Guid>;