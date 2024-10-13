using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class FinancialMovementCategoryFactory : IFinancialMovementCategoryFactory
{
    public Task<FinancialMovementCategory> CreateEntityAsync(FinancialMovementCategoryCreateDto dto) =>
        RebuildEntityAsync(new FinancialMovementCategoryRebuildDto
        {
            Name = dto.Name,
            CreationDateTime = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid()
        });

    public Task<FinancialMovementCategory> RebuildEntityAsync(FinancialMovementCategoryRebuildDto dto) =>
        Task.FromResult(new FinancialMovementCategory(new Name(dto.Name), dto.CreationDateTime, dto.Id));
}