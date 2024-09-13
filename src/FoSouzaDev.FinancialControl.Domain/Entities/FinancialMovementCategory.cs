using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class FinancialMovementCategory(
    Name name,
    DateTimeOffset creationDateTime = default,
    Guid id = default) : Entity(id, creationDateTime)
{
    public Name Name { get; set; } = name;
}