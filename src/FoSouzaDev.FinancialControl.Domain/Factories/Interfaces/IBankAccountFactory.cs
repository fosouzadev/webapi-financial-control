using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IBankAccountFactory
{
    BankAccount CreateEntity(string name, string description, byte type);
    BankAccount RebuildEntity(string name, string description, bool isActive, byte type, decimal balance, DateTimeOffset creationDateTime, Guid id);
}