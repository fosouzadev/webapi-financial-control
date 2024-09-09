namespace FoSouzaDev.FinancialControl.Domain.Entities;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private init; } = id == default ? Guid.NewGuid() : id;
}