using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IFinancialMovementCategoryAppService
{
    Guid Add(AddFinancialMovementCategoryDto dto);
    GetFinancialMovementCategoryDto GetById(Guid id);
    Task UpdateAsync(Guid id, JsonPatchDocument<UpdateFinancialMovementCategoryDto> pathDocument);
    Task RemoveAsync(Guid id);
}