using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IBankAccountAppService : IAppService<BankAccount, BankAccountDto, UpdateBankAccountDto, AddBankAccountDto>
{
}