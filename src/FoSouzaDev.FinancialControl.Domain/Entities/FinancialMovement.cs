using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class FinancialMovement(
    Name name,
    Amount amount,
    FinancialMovementType type,
    FinancialMovementCategory category,
    BankAccount bankAccount,
    DateTimeOffset creationDateTime,
    Guid id) : Entity(id, creationDateTime)
{
    public Name Name { get; set; } = name;
    public Amount Amount { get; set; } = amount;
    public FinancialMovementType Type { get; private set; } = type;
    public FinancialMovementCategory Category { get; set; } = category;
    public BankAccount BankAccount { get; private set; } = bankAccount;
}