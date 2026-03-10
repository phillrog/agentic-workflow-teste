namespace Domain.Entities;

/// <summary>
/// Exemplo de Entidade usando C# 13 Primary Constructors.
/// Para o EF Core, é necessário um construtor sem parâmetros (private/protected).
/// </summary>
public class User(string name, string email)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    // Construtor exigido pelo EF Core para materialização
    protected User() : this(default!, default!) { }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);
        Name = newName;
    }
}