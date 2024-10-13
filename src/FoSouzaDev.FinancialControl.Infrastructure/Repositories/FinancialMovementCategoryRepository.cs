using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.Generic;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository
    (IGenericRepository<FinancialMovementCategoryDataEntity> genericRepository, IFinancialMovementCategoryFactory factory)
    : IFinancialMovementCategoryRepository
{
    public async Task AddAsync(FinancialMovementCategory entity)
    {
        FinancialMovementCategoryDataEntity dataEntity = new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            CreationDateTime = entity.CreationDateTime.UtcDateTime
        };

        await genericRepository.AddAsync(dataEntity);
    }

    public async Task<FinancialMovementCategory> GetByIdAsync(Guid id)
    {
        FinancialMovementCategoryDataEntity dataEntity = await genericRepository.GetByIdAsync(id);
        FinancialMovementCategory entity = null;

        if (dataEntity != null)
            entity = await factory.RebuildEntityAsync(new FinancialMovementCategoryRebuildDto
            {
                Name = dataEntity.Name,
                CreationDateTime = dataEntity.CreationDateTime,
                Id = dataEntity.Id
            });

        return entity;
    }

    public async Task<FinancialMovementCategory> GetByIdOrThrowAsync(Guid id)
    {
        FinancialMovementCategoryDataEntity dataEntity = await genericRepository.GetByIdOrThrowAsync(id);

        return await factory.RebuildEntityAsync(new FinancialMovementCategoryRebuildDto
        {
            Name = dataEntity.Name,
            CreationDateTime = dataEntity.CreationDateTime,
            Id = dataEntity.Id
        });
    }

    public async Task UpdateAsync(FinancialMovementCategory entity) =>
        await genericRepository.UpdateAsync(entity.Id, dataEntity => dataEntity.Name = entity.Name.Value);

    public async Task RemoveAsync(Guid id) =>
        await genericRepository.RemoveAsync(id);
}