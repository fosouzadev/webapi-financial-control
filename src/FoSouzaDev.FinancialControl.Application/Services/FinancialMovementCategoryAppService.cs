using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class FinancialMovementCategoryAppService
    (IFinancialMovementCategoryFactory factory, IFinancialMovementCategoryRepository repository)
    : AppService<FinancialMovementCategory, FinancialMovementCategoryDto, UpdateFinancialMovementCategoryDto, AddFinancialMovementCategoryDto>(factory, repository), IFinancialMovementCategoryAppService
{
    protected override void UpdateEntity(FinancialMovementCategory entity, UpdateFinancialMovementCategoryDto dto)
    {
        entity.Name = new Name(dto.Name);
    }
}