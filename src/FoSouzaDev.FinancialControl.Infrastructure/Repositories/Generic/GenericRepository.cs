using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.MongoDatabase;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories.Generic;

internal sealed class GenericRepository<TDataEntity>(IUserService userService, MongoDbContext dbContext) : IGenericRepository<TDataEntity>
    where TDataEntity : DataEntity
{
    public async Task AddAsync(TDataEntity dataEntity)
    {
        dataEntity.UserId = userService.GetUserId();

        await dbContext.Set<TDataEntity>().AddAsync(dataEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<TDataEntity> GetByIdAsync(Guid id) =>
        await dbContext.Set<TDataEntity>().FirstOrDefaultAsync(a => a.Id == id && a.UserId == userService.GetUserId());

    public async Task<TDataEntity> GetByIdOrThrowAsync(Guid id) =>
        await GetByIdAsync(id) ?? throw new NotFoundException(id);

    public async Task UpdateAsync(Guid id, Action<TDataEntity> setChanges)
    {
        TDataEntity dataEntity = await GetByIdOrThrowAsync(id);

        setChanges(dataEntity);

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        TDataEntity dataEntity = await GetByIdOrThrowAsync(id);

        dbContext.Remove(dataEntity);
        await dbContext.SaveChangesAsync();
    }
}