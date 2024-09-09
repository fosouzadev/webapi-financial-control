using FoSouzaDev.FinancialControl.Application.Factories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record UpdateFinancialMovementCategoryDto
{
    public required string Name { get; set; }

    public static explicit operator UpdateFinancialMovementCategoryDto(FinancialMovementCategory entity) =>
        FinancialMovementCategoryFactory.DomainEntityToUpdateDto(entity);
}