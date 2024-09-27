using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IFinancialMovementCategoryAppService
{
    Task<Guid> AddAsync(AddFinancialMovementCategoryDto dto);
    Task<GetFinancialMovementCategoryDto> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, JsonPatchDocument<UpdateFinancialMovementCategoryDto> jsonPathDocument);
    Task RemoveAsync(Guid id);
}