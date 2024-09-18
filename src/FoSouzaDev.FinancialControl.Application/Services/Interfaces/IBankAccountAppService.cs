using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IBankAccountAppService
{
    Task<Guid> AddAsync(AddBankAccountDto dto);
    Task<BankAccountDto> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, JsonPatchDocument<UpdateBankAccountDto> pathDocument);
    Task RemoveAsync(Guid id);
}