using MediatR;

namespace Application.Products.Commands;

/// <summary>
/// Exemplo de Command usando C# 12/13 Primary Constructors (disponíveis no .NET 10).
/// </summary>
public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    // Injeção via Primary Constructor na classe (Opcional, mas recomendado no .NET 10)
    // Se preferir o tradicional, use o construtor abaixo:
    
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Simulação de lógica de domínio
        var id = Guid.NewGuid();
        return await Task.FromResult(id);
    }
}