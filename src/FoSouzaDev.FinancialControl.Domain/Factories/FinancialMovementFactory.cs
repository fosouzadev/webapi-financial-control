using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class FinancialMovementFactory : IFinancialMovementFactory
{
    public FinancialMovement CreateEntityAsync(
        string name, decimal amount, FinancialMovementType type, FinancialMovementCategory category, BankAccount bankAccount) =>
        new(new Name(name), new Amount(amount), type, category, bankAccount, DateTimeOffset.UtcNow, Guid.NewGuid());

    public FinancialMovement RebuildEntity(
        string name, decimal amount, FinancialMovementType type, FinancialMovementCategory category, BankAccount bankAccount, DateTimeOffset creationDateTime, Guid id) =>
        new(new Name(name), new Amount(amount), type, category, bankAccount, creationDateTime, id);
}
