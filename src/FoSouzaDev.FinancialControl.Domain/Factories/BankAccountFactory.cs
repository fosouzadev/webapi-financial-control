using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class BankAccountFactory : FactoryBase, IBankAccountFactory
{
    public BankAccount CreateEntity(string name, string description, byte type)
    {
        base.ThrowIfIsNotValidValue<BankAccountType>(type);

        return new(new Name(name), description, true, (BankAccountType)type, 0, DateTimeOffset.UtcNow, Guid.NewGuid());
    }

    public BankAccount RebuildEntity(
        string name, string description, bool isActive, byte type, decimal balance, DateTimeOffset creationDateTime, Guid id)
    {
        base.ThrowIfIsNotValidValue<BankAccountType>(type);

        return new(new Name(name), description, isActive, (BankAccountType)type, balance, creationDateTime, id);
    }
}