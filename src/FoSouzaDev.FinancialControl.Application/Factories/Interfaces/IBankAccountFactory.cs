using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.Factories.Interfaces;

public interface IBankAccountFactory : IAppFactory<BankAccount, BankAccountDto, UpdateBankAccountDto, AddBankAccountDto>
{
}