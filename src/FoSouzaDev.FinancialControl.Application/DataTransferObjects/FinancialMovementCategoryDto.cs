using FoSouzaDev.FinancialControl.Application.Factories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record FinancialMovementCategoryDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }

    public static explicit operator FinancialMovementCategoryDto(FinancialMovementCategory entity) =>
        FinancialMovementCategoryFactory.DomainEntityToDto(entity);
}