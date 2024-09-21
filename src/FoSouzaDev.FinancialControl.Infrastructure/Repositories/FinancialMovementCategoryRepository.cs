using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using FoSouzaDev.FinancialControl.Infrastructure.Repositories.MongoDatabase;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository
    (IUserService userService, MongoDbContext dbContext)
    : IFinancialMovementCategoryRepository
{
    private readonly Dictionary<Guid, List<FinancialMovementCategory>> _financialMovementCategories = new();

    public void Add(FinancialMovementCategory entity)
    {
        FinancialMovementCategoryDataEntity dataEntity = new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            CreationDateTime = entity.CreationDateTime.UtcDateTime,
            UserId = userService.GetUserId()
        };

        dbContext.FinancialMovementCategories.Add(dataEntity);
        dbContext.SaveChanges();
    }

    public FinancialMovementCategory GetById(Guid id)
    {
        FinancialMovementCategoryDataEntity dataEntity = dbContext.FinancialMovementCategories.FirstOrDefault(a =>  a.Id == id);

        if (dataEntity == null)
            return null;

        return new(new Name(dataEntity.Name), dataEntity.CreationDateTime, dataEntity.Id);
    }

    public FinancialMovementCategory GetByIdOrThrow(Guid id)
    {
        FinancialMovementCategory entity = GetById(id);

        if (entity == null)
            throw new NotFoundException(id);

        return entity;
    }

    public async Task UpdateAsync(FinancialMovementCategory entity)
    {
        if (_financialMovementCategories.TryGetValue(userService.GetUserId(), out var financialMovementCategories))
        {
            financialMovementCategories.Remove(entity);
            financialMovementCategories.Add(entity);
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        if (_financialMovementCategories.TryGetValue(userService.GetUserId(), out var financialMovementCategories))
        {
            FinancialMovementCategory entity = financialMovementCategories.Single(a => a.Id == id);
            financialMovementCategories.Remove(entity);
        }
    }
}