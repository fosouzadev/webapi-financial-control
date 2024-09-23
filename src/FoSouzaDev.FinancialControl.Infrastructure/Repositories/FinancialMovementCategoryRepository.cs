using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.MongoDatabase;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository
    (IUserService userService, MongoDbContext dbContext)
    : IFinancialMovementCategoryRepository
{
    private async Task<FinancialMovementCategoryDataEntity> GetDataEntityAsync(Guid id) =>
        await dbContext.FinancialMovementCategories.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userService.GetUserId());

    public async Task AddAsync(FinancialMovementCategory entity)
    {
        FinancialMovementCategoryDataEntity dataEntity = new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            CreationDateTime = entity.CreationDateTime.UtcDateTime,
            UserId = userService.GetUserId()
        };

        await dbContext.FinancialMovementCategories.AddAsync(dataEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<FinancialMovementCategory> GetByIdAsync(Guid id)
    {
        FinancialMovementCategoryDataEntity dataEntity = await GetDataEntityAsync(id);

        if (dataEntity == null)
            return null;

        return new(new Name(dataEntity.Name), dataEntity.CreationDateTime, dataEntity.Id);
    }

    public async Task<FinancialMovementCategory> GetByIdOrThrowAsync(Guid id)
    {
        return await GetByIdAsync(id) ?? throw new NotFoundException(id);
    }

    public async Task UpdateAsync(FinancialMovementCategory entity)
    {
        FinancialMovementCategoryDataEntity dataEntity = await GetDataEntityAsync(entity.Id);

        dataEntity.Name = entity.Name.Value;
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        FinancialMovementCategoryDataEntity dataEntity = await GetDataEntityAsync(id);

        dbContext.Remove(dataEntity);
        await dbContext.SaveChangesAsync();
    }
}