using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class FinancialMovementCategory(
    Name name,
    DateTimeOffset creationDateTime,
    Guid id) : Entity(id, creationDateTime)
{
    public Name Name { get; set; } = name;
}