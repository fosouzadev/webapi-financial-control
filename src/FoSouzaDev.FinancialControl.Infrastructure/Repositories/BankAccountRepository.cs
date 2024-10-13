using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
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
            entity = await factory.RebuildEntityAsync(new BankAccountRebuildDto
            {
                Name = dataEntity.Name,
                Description = dataEntity.Description,
                IsActive = dataEntity.IsActive,
                Type = dataEntity.Type,
                Balance = dataEntity.Balance,
                CreationDateTime = dataEntity.CreationDateTime,
                Id = dataEntity.Id
            });

        return entity;
    }

    public async Task<BankAccount> GetByIdOrThrowAsync(Guid id)
    {
        BankAccountDataEntity dataEntity = await genericRepository.GetByIdOrThrowAsync(id);

        return await factory.RebuildEntityAsync(new BankAccountRebuildDto
        {
            Name = dataEntity.Name,
            Description = dataEntity.Description,
            IsActive = dataEntity.IsActive,
            Type = dataEntity.Type,
            Balance = dataEntity.Balance,
            CreationDateTime = dataEntity.CreationDateTime,
            Id = dataEntity.Id
        });
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