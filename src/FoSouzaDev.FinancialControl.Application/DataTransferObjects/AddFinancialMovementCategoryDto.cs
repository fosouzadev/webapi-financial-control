using FoSouzaDev.FinancialControl.Application.Factories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record AddFinancialMovementCategoryDto
{
    public required string Name { get; init; }

    public static explicit operator FinancialMovementCategory(AddFinancialMovementCategoryDto dto) =>
        FinancialMovementCategoryFactory.AddDtoToDomainEntity(dto);
}