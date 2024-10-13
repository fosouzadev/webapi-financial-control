using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class FinancialMovementCategoryFactory : IFinancialMovementCategoryFactory
{
    public FinancialMovementCategory CreateEntity(FinancialMovementCategoryCreateDto dto) =>
        RebuildEntity(new FinancialMovementCategoryRebuildDto
        {
            Name = dto.Name,
            CreationDateTime = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid()
        });

    public FinancialMovementCategory RebuildEntity(FinancialMovementCategoryRebuildDto dto) =>
        new(new Name(dto.Name), dto.CreationDateTime, dto.Id);
}