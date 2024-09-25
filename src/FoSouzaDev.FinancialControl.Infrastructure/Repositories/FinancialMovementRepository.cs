using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.Generic;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal class FinancialMovementRepository
    (IGenericRepository<FinancialMovementDataEntity> genericRepository, IFinancialMovementFactory factory)
    : IFinancialMovementRepository
{
    public async Task AddAsync(FinancialMovement entity)
    {
        FinancialMovementDataEntity dataEntity = new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            Amount = entity.Amount.Value,
            Type = (byte)entity.Type,
            CategoryId = entity.Category.Id,
            BankAccountId = entity.BankAccount.Id,
            CreationDateTime = entity.CreationDateTime.UtcDateTime
        };

        await genericRepository.AddAsync(dataEntity);
    }

    public async Task<FinancialMovement> GetByIdAsync(Guid id)
    {
        FinancialMovementDataEntity dataEntity = await genericRepository.GetByIdAsync(id);
        FinancialMovement entity = null;

        if (dataEntity != null)
            entity = await factory.RebuildEntityAsync(
                dataEntity.Name,
                dataEntity.Amount,
                dataEntity.Type,
                dataEntity.CategoryId,
                dataEntity.BankAccountId,
                dataEntity.CreationDateTime,
                dataEntity.Id);

        return entity;
    }

    public async Task<FinancialMovement> GetByIdOrThrowAsync(Guid id)
    {
        FinancialMovementDataEntity dataEntity = await genericRepository.GetByIdOrThrowAsync(id);

        return await factory.RebuildEntityAsync(
            dataEntity.Name,
            dataEntity.Amount,
            dataEntity.Type,
            dataEntity.CategoryId,
            dataEntity.BankAccountId,
            dataEntity.CreationDateTime,
            dataEntity.Id);
    }

    public async Task UpdateAsync(FinancialMovement entity) =>
        await genericRepository.UpdateAsync(entity.Id, dataEntity =>
        {
            dataEntity.Name = entity.Name.Value;
            dataEntity.Amount = entity.Amount.Value;
            dataEntity.CategoryId = entity.Category.Id;
        });
}