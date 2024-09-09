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

    public static UpdateFinancialMovementCategoryDto DomainEntityToUpdateDto(FinancialMovementCategory entity) =>
        new()
        {
            Name = entity.Name.Value
        };

    public static FinancialMovementCategory AddDtoToDomainEntity(AddFinancialMovementCategoryDto dto) =>
        new(new Name(dto.Name));
}