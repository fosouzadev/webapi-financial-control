using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.Factories;

internal static class FinancialMovementCategoryFactory
{
    public static FinancialMovementCategoryDto DomainEntityToDto(FinancialMovementCategory entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static AddOrUpdateFinancialMovementCategoryDto DomainEntityToAddOrUpdateDto(FinancialMovementCategory entity) =>
        new()
        {
            Name = entity.Name
        };

    public static FinancialMovementCategory AddOrUpdateDtoToDomainEntity(AddOrUpdateFinancialMovementCategoryDto dto) =>
        new(dto.Name);
}