using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class BankAccount(
    Name name,
    string description,
    bool isActive,
    BankAccountType type,
    decimal balance = 0,
    DateTimeOffset creationDateTime = default,
    Guid id = default) : Entity(id, creationDateTime)
{
    private List<FinancialMovement> _financialMovements = new();

    public Name Name { get; set; } = name;
    public string Description { get; set; } = description;
    public bool IsActive { get; set; } = isActive;
    public decimal Balance { get; private set; } = balance;
    public BankAccountType Type { get; private init; } = type;

    public void AddFinancialMovement(
        Name name, Amount amount, FinancialMovementType type, FinancialMovementCategory category)
    {
        _financialMovements.Add(new FinancialMovement(name, amount, type, category));
    }
}