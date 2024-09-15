using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class BankAccountFactory : IBankAccountFactory
{
    public BankAccount CreateEntity(string name, string description, BankAccountType type) =>
        new(new Name(name), description, true, type, 0, DateTimeOffset.UtcNow, Guid.NewGuid());

    public BankAccount RebuildEntity(
        string name, string description, bool isActive, BankAccountType type, decimal balance, DateTimeOffset creationDateTime, Guid id) =>
        new(new Name(name), description, isActive, type, balance, creationDateTime, id);
}