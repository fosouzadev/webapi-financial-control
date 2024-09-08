using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Application.Factories;

internal static class FinancialMovementCategoryFactory
{
    public static FinancialMovementCategoryDto DomainEntityToDto(FinancialMovementCategory entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name.Value
        };

    public static AddOrUpdateFinancialMovementCategoryDto DomainEntityToAddOrUpdateDto(FinancialMovementCategory entity) =>
        new()
        {
            Name = entity.Name.Value
        };

    public static FinancialMovementCategory AddOrUpdateDtoToDomainEntity(AddOrUpdateFinancialMovementCategoryDto dto) =>
        new(new Name(dto.Name));
}