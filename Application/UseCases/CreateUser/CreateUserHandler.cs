using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UseCases.CreateUser;

/// <summary>
/// Implementação corrigida usando Primary Constructors para Injeção de Dependência.
/// </summary>
public class CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);
        
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}