using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IFinancialMovementAppService
{
    Task<Guid> AddAsync(AddFinancialMovementDto dto);
    Task<FinancialMovementDto> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, JsonPatchDocument<UpdateFinancialMovementDto> pathDocument);
}