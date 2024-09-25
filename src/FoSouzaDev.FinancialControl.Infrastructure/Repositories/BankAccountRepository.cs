using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.Generic;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class BankAccountRepository
    (IGenericRepository<BankAccountDataEntity> genericRepository, IBankAccountFactory factory)
    : IBankAccountRepository
{
    public async Task AddAsync(BankAccount entity)
    {
        BankAccountDataEntity dataEntity = new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            Description = entity.Description,
            Type = (byte)entity.Type,
            Balance = entity.Balance,
            IsActive = entity.IsActive,
            CreationDateTime = entity.CreationDateTime.UtcDateTime
        };

        await genericRepository.AddAsync(dataEntity);
    }

    public async Task<BankAccount> GetByIdAsync(Guid id)
    {
        BankAccountDataEntity dataEntity = await genericRepository.GetByIdAsync(id);
        BankAccount entity = null;

        if (dataEntity != null)
            entity = factory.RebuildEntity(
                dataEntity.Name,
                dataEntity.Description,
                dataEntity.IsActive,
                dataEntity.Type,
                dataEntity.Balance,
                dataEntity.CreationDateTime,
                dataEntity.Id);

        return entity;
    }

    public async Task<BankAccount> GetByIdOrThrowAsync(Guid id)
    {
        BankAccountDataEntity dataEntity = await genericRepository.GetByIdOrThrowAsync(id);

        return factory.RebuildEntity(
            dataEntity.Name,
            dataEntity.Description,
            dataEntity.IsActive,
            dataEntity.Type,
            dataEntity.Balance,
            dataEntity.CreationDateTime,
            dataEntity.Id);
    }

    public async Task UpdateAsync(BankAccount entity) =>
        await genericRepository.UpdateAsync(entity.Id, dataEntity =>
        {
            dataEntity.Name = entity.Name.Value;
            dataEntity.Description = entity.Description;
            dataEntity.IsActive = entity.IsActive;
        });

    public async Task RemoveAsync(Guid id) =>
        await genericRepository.RemoveAsync(id);
}