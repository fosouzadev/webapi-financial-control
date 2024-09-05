using FoSouzaDev.FinancialControl.Application.Factories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record AddOrUpdateFinancialMovementCategoryDto
{
    public required string Name { get; set; }

    public static explicit operator AddOrUpdateFinancialMovementCategoryDto(FinancialMovementCategory entity) =>
        FinancialMovementCategoryFactory.DomainEntityToAddOrUpdateDto(entity);

    public static explicit operator FinancialMovementCategory(AddOrUpdateFinancialMovementCategoryDto dto) =>
        FinancialMovementCategoryFactory.AddOrUpdateDtoToDomainEntity(dto);
}