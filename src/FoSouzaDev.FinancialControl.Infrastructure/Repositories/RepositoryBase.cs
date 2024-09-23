using FoSouzaDev.Common.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal abstract class RepositoryBase<TDataEntity>
    (DbSet<TDataEntity> dataEntity, IUserService userService) :
    IAddRepository<FinancialMovementCategory>,
    IGetRepository<FinancialMovementCategory>
    where TDataEntity : DataEntity
{
    protected DbSet<TDataEntity> DataEntity { get; init; } = dataEntity;

    private async Task<TDataEntity> GetDataEntityAsync(Guid id) =>
        await DataEntity.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userService.GetUserId());

    public Task AddAsync(FinancialMovementCategory entity)
    {
        // necessário factory para traduzir entity -> dataEntity e dataEntity -> entity
        throw new NotImplementedException();
    }

    public Task<FinancialMovementCategory> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<FinancialMovementCategory> GetByIdOrThrowAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}