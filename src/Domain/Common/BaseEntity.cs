namespace Domain.Common;

/// <summary>
/// Base class para entidades DDD. 
/// No .NET 10, usamos record ou classes com propriedades 'init' para imutabilidade controlada.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    // Domain Events para manter o DDD puro
    private readonly List<BaseEvent> _domainEvents = new();
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract record BaseEvent;