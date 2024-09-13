namespace FoSouzaDev.FinancialControl.Domain.Entities;

public abstract class Entity(Guid id, DateTimeOffset creationDateTime)
{
    public Guid Id { get; private init; } = id == default ? Guid.NewGuid() : id;
    public DateTimeOffset CreationDateTime { get; private init; } = creationDateTime == default ? DateTimeOffset.UtcNow : creationDateTime;
}