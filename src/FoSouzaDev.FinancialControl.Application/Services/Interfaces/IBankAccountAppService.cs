using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IBankAccountAppService : IAppServiceBase<BankAccount, BankAccountDto, UpdateBankAccountDto, AddBankAccountDto>
{
    Task<Guid> AddFinancialMovementAsync(Guid userId, Guid bankAccountId, AddFinancialMovementDto dto);
}