using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class FinancialMovementCategory(Name name, Guid? id = null)
{
    public Guid Id { get; private init; } = id ?? Guid.NewGuid();
    public Name Name { get; set; } = name;
}