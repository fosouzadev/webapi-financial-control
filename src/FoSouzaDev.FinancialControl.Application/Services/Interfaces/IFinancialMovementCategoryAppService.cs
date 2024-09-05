using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IFinancialMovementCategoryAppService
{
    Task<Guid> AddAsync(Guid userId, AddOrUpdateFinancialMovementCategoryDto dto);
    Task<FinancialMovementCategoryDto> GetByIdAsync(Guid userId, Guid id);
    Task UpdateAsync(Guid userId, Guid id, JsonPatchDocument<AddOrUpdateFinancialMovementCategoryDto> pathDocument);
    Task RemoveAsync(Guid userId, Guid id);
}