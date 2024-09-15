using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IBankAccountFactory
{
    BankAccount CreateEntity(string name, string description, BankAccountType type);
    BankAccount RebuildEntity(string name, string description, bool isActive, BankAccountType type, decimal balance, DateTimeOffset creationDateTime, Guid id);
}