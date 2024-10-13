using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IBankAccountFactory
    : IDomainFactory<BankAccount, BankAccountCreateDto, BankAccountRebuildDto>
{
}