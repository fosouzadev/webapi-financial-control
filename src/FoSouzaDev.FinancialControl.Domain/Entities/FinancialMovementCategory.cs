using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class FinancialMovementCategory(Name name, Guid id = default) : Entity(id)
{
    public Name Name { get; set; } = name;
}