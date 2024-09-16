using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository(IUserService service) : IFinancialMovementCategoryRepository
{
    private readonly Dictionary<Guid, List<FinancialMovementCategory>> _financialMovementCategories = new();

    public async Task AddAsync(FinancialMovementCategory entity)
    {
        if (_financialMovementCategories.TryGetValue(service.GetUserId(), out var financialMovementCategories))
            financialMovementCategories.Add(entity);
    }

    public async Task<FinancialMovementCategory> GetByIdAsync(Guid id)
    {
        if (_financialMovementCategories.TryGetValue(service.GetUserId(), out var financialMovementCategories))
            return financialMovementCategories.SingleOrDefault(a => a.Id == id);

        return null;
    }

    public async Task<FinancialMovementCategory> GetByIdOrThrowAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            throw new NotFoundException(id);

        return entity;
    }

    public async Task UpdateAsync(FinancialMovementCategory entity)
    {
        if (_financialMovementCategories.TryGetValue(service.GetUserId(), out var financialMovementCategories))
        {
            financialMovementCategories.Remove(entity);
            financialMovementCategories.Add(entity);
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        if (_financialMovementCategories.TryGetValue(service.GetUserId(), out var financialMovementCategories))
        {
            FinancialMovementCategory entity = financialMovementCategories.Single(a => a.Id == id);
            financialMovementCategories.Remove(entity);
        }
    }
}