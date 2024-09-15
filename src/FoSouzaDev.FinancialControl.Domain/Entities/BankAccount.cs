using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class BankAccount(
    Name name,
    string description,
    bool isActive,
    BankAccountType type,
    decimal balance,
    DateTimeOffset creationDateTime,
    Guid id) : Entity(id, creationDateTime)
{
    //private List<FinancialMovement> _financialMovements = new();

    public Name Name { get; set; } = name;
    public string Description { get; set; } = description;
    public bool IsActive { get; set; } = isActive;
    public decimal Balance { get; private set; } = balance;
    public BankAccountType Type { get; private init; } = type;

    public Guid AddFinancialMovement(
        Name name, Amount amount, FinancialMovementType type, FinancialMovementCategory category)
    {
        //FinancialMovement financialMovement = new(name, amount, type, category);

        //_financialMovements.Add(financialMovement);

        //return financialMovement.Id;

        return Guid.Empty;
    }
}