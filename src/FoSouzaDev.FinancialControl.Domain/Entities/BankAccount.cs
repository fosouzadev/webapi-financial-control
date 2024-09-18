using FoSouzaDev.Common.Domain.Entities;
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
    public Name Name { get; set; } = name;
    public string Description { get; set; } = description;
    public bool IsActive { get; set; } = isActive;
    public decimal Balance { get; private set; } = balance;
    public BankAccountType Type { get; private init; } = type;
}