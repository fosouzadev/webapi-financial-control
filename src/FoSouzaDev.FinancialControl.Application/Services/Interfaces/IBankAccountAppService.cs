using FoSouzaDev.FinancialControl.Application.DataTransferObjects;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IBankAccountAppService : IAllActionsAppService<AddBankAccountDto, GetBankAccountDto, UpdateBankAccountDto>
{
}