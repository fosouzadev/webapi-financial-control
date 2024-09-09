using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class BankAccount(
    Name name,
    string description,
    bool isActive,
    BankAccountType type,
    DateTimeOffset creationDateTime = default,
    decimal balance = 0,
    Guid id = default) : Entity(id)
{
    public Name Name { get; set; } = name;
    public string Description { get; set; } = description;
    public bool IsActive { get; set; } = isActive;
    public decimal Balance { get; private set; } = balance;
    public BankAccountType Type { get; private init; } = type;
    public DateTimeOffset CreationDateTime { get; private init; } = creationDateTime == default ? DateTimeOffset.UtcNow : creationDateTime;

    // criar método para adicionar lançamento e atualizar saldo
}